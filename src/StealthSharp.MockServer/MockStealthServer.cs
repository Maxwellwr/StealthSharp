#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MockStealthServer.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.MockServer
{
    /// <summary>
    ///     In-process fake of the Stealth API server, wire compatible with what <c>StealthSharpClient</c> expects.
    ///     Useful for tests and benchmarks that would otherwise need a real Stealth + UO client.
    /// </summary>
    /// <remarks>
    ///     The protocol is reverse engineered from the client code, not from Stealth documentation:
    ///     <list type="bullet">
    ///         <item>
    ///             Discovery: client sends <c>04 00 EF BE AD DE</c> to the discovery port, server answers
    ///             <c>02 00 &lt;port:u16&gt;</c> with the port of the API connection.
    ///         </item>
    ///         <item>
    ///             Request: <c>&lt;length:u32&gt; &lt;type:u16&gt; &lt;correlationId:u16&gt; &lt;body&gt;</c>, where length
    ///             counts everything after the length field (<c>4 + body</c>).
    ///         </item>
    ///         <item>
    ///             Response: same framing with type <see cref="PacketType.SCReturnValue" /> and the request
    ///             correlation id. Commands the client does not await (void methods) get no response.
    ///         </item>
    ///         <item>
    ///             Event: type <see cref="PacketType.SCExecEventProc" />, no correlation id, length is
    ///             <c>2 + payload</c>.
    ///         </item>
    ///     </list>
    ///     Requests without a registered handler are counted in <see cref="UnhandledRequests" /> and not answered,
    ///     so a client awaiting such a request will hang.
    /// </remarks>
    public sealed class MockStealthServer : IDisposable
    {
        private const int HEADER_SIZE = 6;
        private static readonly byte[] DiscoveryMagic = { 0x04, 0x00, 0xEF, 0xBE, 0xAD, 0xDE };

        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<PacketType, Func<MockRequest, object?>> _handlers = new();
        private readonly ConcurrentDictionary<int, Connection> _connections = new();
        private readonly ConcurrentDictionary<PacketType, long> _requestCounts = new();
        private readonly IMarshaler _marshaler;
        // Marshaler / ReflectionCache are not thread safe.
        private readonly object _marshalLock = new();
        private readonly TcpListener _discoveryListener;
        private readonly TcpListener _apiListener;
        private long _requestCount;
        private long _unhandledRequests;
        private int _nextConnectionId;
        private bool _started;

        public MockStealthServer(IPAddress? address = null)
        {
            address ??= IPAddress.Loopback;
            var services = new ServiceCollection();
            services.AddStealthSharpSerialization(o =>
            {
                o.ArrayCountType = typeof(uint);
                o.StringSizeType = typeof(uint);
            });
            _marshaler = services.BuildServiceProvider().GetRequiredService<IMarshaler>();

            _discoveryListener = new TcpListener(address, 0);
            _apiListener = new TcpListener(address, 0);

            MockDefaults.Register(this);
        }

        /// <summary>Port to put into <c>StealthOptions.Port</c>.</summary>
        public int DiscoveryPort => ((IPEndPoint)_discoveryListener.LocalEndpoint).Port;

        /// <summary>Port of the API connection, returned to clients by the discovery request.</summary>
        public int ApiPort => ((IPEndPoint)_apiListener.LocalEndpoint).Port;

        /// <summary>Total number of requests received.</summary>
        public long RequestCount => Interlocked.Read(ref _requestCount);

        /// <summary>Number of requests that had no registered handler.</summary>
        public long UnhandledRequests => Interlocked.Read(ref _unhandledRequests);

        public long GetRequestCount(PacketType packetType)
        {
            return _requestCounts.TryGetValue(packetType, out var count) ? Interlocked.Read(ref count) : 0;
        }

        /// <summary>
        ///     Registers a handler. The returned object is serialized with the same marshaler settings as the
        ///     client and sent as the response body; <see langword="null" /> means "send no response" and a
        ///     <see cref="byte" />[] is sent as is.
        /// </summary>
        public MockStealthServer On(PacketType packetType, Func<MockRequest, object?> handler)
        {
            _handlers[packetType] = handler;
            return this;
        }

        /// <summary>
        ///     Always answers <paramref name="packetType" /> with <paramref name="response" />. The response is
        ///     serialized once, so serialization cost does not show up in benchmarks.
        /// </summary>
        public MockStealthServer RespondWith(PacketType packetType, object response)
        {
            var bytes = Serialize(response);
            return On(packetType, _ => bytes);
        }

        /// <summary>Starts listening. Ports are available after this call.</summary>
        public void Start()
        {
            if (_started) throw new InvalidOperationException("Already started");
            _started = true;
            _discoveryListener.Start();
            _apiListener.Start();
            _ = Task.Run(() => AcceptLoopAsync(_discoveryListener, HandleDiscoveryAsync));
            _ = Task.Run(() => AcceptLoopAsync(_apiListener, HandleApiConnectionAsync));
        }

        /// <summary>Sends an event to every connected client.</summary>
        public async Task PushEventAsync(ServerEventData eventData)
        {
            var payload = Serialize(eventData);
            var frame = new byte[HEADER_SIZE + payload.Length];
            BinaryPrimitives.WriteUInt32LittleEndian(frame, (uint)(2 + payload.Length));
            BinaryPrimitives.WriteUInt16LittleEndian(frame.AsSpan(4), (ushort)PacketType.SCExecEventProc);
            payload.CopyTo(frame, HEADER_SIZE);

            foreach (var connection in _connections.Values)
                await connection.WriteAsync(frame, _cts.Token).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _cts.Cancel();
            _discoveryListener.Stop();
            _apiListener.Stop();
            foreach (var connection in _connections.Values)
                connection.Dispose();
            _cts.Dispose();
        }

        private byte[] Serialize(object value)
        {
            if (value is byte[] raw) return raw;
            lock (_marshalLock)
            {
                using var result = _marshaler.Serialize(value);
                return result.Memory.ToArray();
            }
        }

        private async Task AcceptLoopAsync(TcpListener listener, Func<TcpClient, Task> handle)
        {
            while (!_cts.IsCancellationRequested)
            {
                TcpClient client;
                try
                {
                    client = await listener.AcceptTcpClientAsync(_cts.Token).ConfigureAwait(false);
                }
                catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException or SocketException)
                {
                    return;
                }

                client.NoDelay = true;
                _ = Task.Run(() => handle(client));
            }
        }

        private async Task HandleDiscoveryAsync(TcpClient client)
        {
            using (client)
            {
                try
                {
                    var stream = client.GetStream();
                    var request = new byte[DiscoveryMagic.Length];
                    await stream.ReadExactlyAsync(request, _cts.Token).ConfigureAwait(false);
                    if (!request.AsSpan().SequenceEqual(DiscoveryMagic))
                        return;

                    var response = new byte[4];
                    BinaryPrimitives.WriteUInt16LittleEndian(response, 2);
                    BinaryPrimitives.WriteUInt16LittleEndian(response.AsSpan(2), (ushort)ApiPort);
                    await stream.WriteAsync(response, _cts.Token).ConfigureAwait(false);
                }
                catch (Exception e) when (e is IOException or OperationCanceledException or ObjectDisposedException)
                {
                }
            }
        }

        private async Task HandleApiConnectionAsync(TcpClient client)
        {
            var id = Interlocked.Increment(ref _nextConnectionId);
            var connection = new Connection(client);
            _connections[id] = connection;
            try
            {
                var stream = client.GetStream();
                var header = new byte[HEADER_SIZE];
                while (!_cts.IsCancellationRequested)
                {
                    await stream.ReadExactlyAsync(header, _cts.Token).ConfigureAwait(false);
                    var length = BinaryPrimitives.ReadUInt32LittleEndian(header);
                    var packetType = (PacketType)BinaryPrimitives.ReadUInt16LittleEndian(header.AsSpan(4));
                    if (length < 4)
                        throw new InvalidDataException($"Bad packet length {length}");

                    var payload = new byte[length - 2];
                    await stream.ReadExactlyAsync(payload, _cts.Token).ConfigureAwait(false);
                    var request = new MockRequest(packetType, BinaryPrimitives.ReadUInt16LittleEndian(payload),
                        payload.AsMemory(2));

                    Interlocked.Increment(ref _requestCount);
                    _requestCounts.AddOrUpdate(packetType, 1, (_, c) => c + 1);

                    if (!_handlers.TryGetValue(packetType, out var handler))
                    {
                        Interlocked.Increment(ref _unhandledRequests);
                        continue;
                    }

                    var response = handler(request);
                    if (response is null)
                        continue;

                    var body = Serialize(response);
                    var frame = new byte[HEADER_SIZE + 2 + body.Length];
                    BinaryPrimitives.WriteUInt32LittleEndian(frame, (uint)(4 + body.Length));
                    BinaryPrimitives.WriteUInt16LittleEndian(frame.AsSpan(4), (ushort)PacketType.SCReturnValue);
                    BinaryPrimitives.WriteUInt16LittleEndian(frame.AsSpan(6), request.CorrelationId);
                    body.CopyTo(frame, HEADER_SIZE + 2);
                    await connection.WriteAsync(frame, _cts.Token).ConfigureAwait(false);
                }
            }
            catch (Exception e) when (e is IOException or OperationCanceledException or ObjectDisposedException)
            {
            }
            finally
            {
                _connections.TryRemove(id, out _);
                connection.Dispose();
            }
        }

        private sealed class Connection : IDisposable
        {
            private readonly TcpClient _client;
            private readonly SemaphoreSlim _writeLock = new(1, 1);

            public Connection(TcpClient client)
            {
                _client = client;
            }

            public async Task WriteAsync(byte[] frame, CancellationToken token)
            {
                await _writeLock.WaitAsync(token).ConfigureAwait(false);
                try
                {
                    await _client.GetStream().WriteAsync(frame, token).ConfigureAwait(false);
                }
                finally
                {
                    _writeLock.Release();
                }
            }

            public void Dispose()
            {
                _client.Dispose();
            }
        }
    }
}
