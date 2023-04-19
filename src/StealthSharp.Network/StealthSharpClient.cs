#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClient.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Drenalol.WaitingDictionary;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoreLinq.Extensions;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Network
{
    public class StealthSharpClient : IStealthSharpClient
    {
        private readonly IPacketCorrelationGenerator<ushort> _correlationGenerator;
        private readonly CancellationTokenSource _baseCancellationTokenSource;
        private readonly CancellationToken _baseCancellationToken;
        private readonly TcpClient _tcpClient;
        private readonly WaitingDictionary<ushort, SerializationResult> _completeResponses;
        private readonly IMarshaler _marshaler;
        private readonly StealthSharpClientOptions _options;
        private readonly ILogger<StealthSharpClient>? _logger;
        private PipeReader _networkStreamPipeReader;
        private PipeWriter _networkStreamPipeWriter;
        private bool _disposing;
        private readonly List<IObserver<ServerEventData>> _observers = new();

        PipeReader IDuplexPipe.Input =>
            _networkStreamPipeReader;

        PipeWriter IDuplexPipe.Output =>
            _networkStreamPipeWriter;

        public void Connect(IPAddress address, int port)
        {
            _tcpClient.Connect(address, port);
            SetupTcpClient();
            SetupTasks();
        }

        public StealthSharpClient(
            IMarshaler marshaler,
            IPacketCorrelationGenerator<ushort> correlationGenerator,
            IOptions<StealthSharpClientOptions>? options,
            ILogger<StealthSharpClient>? logger)
        {
            var pipe = new Pipe();
            _correlationGenerator = correlationGenerator;
            _tcpClient = new TcpClient();
            _logger = logger;
            _options = options?.Value ?? StealthSharpClientOptions.Default;
            _baseCancellationTokenSource = new CancellationTokenSource();
            _baseCancellationToken = _baseCancellationTokenSource.Token;
            var middleware = new MiddlewareBuilder<SerializationResult>()
                .RegisterCancellationActionInWait((tcs, hasOwnToken) =>
                {
                    if (_disposing || hasOwnToken)
                        tcs.TrySetCanceled();
                    else if (!_disposing)
                        tcs.TrySetException(StealthSharpException.ConnectionBroken());
                });
            _completeResponses = new WaitingDictionary<ushort, SerializationResult>(middleware);
            _marshaler = marshaler;
            _networkStreamPipeReader = pipe.Reader;
            _networkStreamPipeWriter = pipe.Writer;
        }


        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient" />
        ///     object.
        /// </summary>
        /// <param name="packetType"></param>
        /// <param name="body"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharpException"></exception>
        public async Task<(bool status, ushort correlationId)> SendAsync(PacketType packetType,
            object? body = null,
            CancellationToken token = default)
        {
            try
            {
                if (_disposing)
                    throw new ObjectDisposedException(nameof(_tcpClient));

                using var serializedBody = body is not null ? _marshaler.Serialize(body) : new SerializationResult(0);
                var correlationId = _correlationGenerator.GetNextCorrelationId();
                var packetHeader = new PacketHeader
                {
                    PacketType = packetType,
                    Length = (uint)(serializedBody.Memory.Length + 4)
                };
                using var serializedHeader = _marshaler.Serialize(packetHeader);
                using var correlation = _marshaler.Serialize(correlationId);

                var writeResult =
                    await _networkStreamPipeWriter.WriteAsync(serializedHeader.Memory, _baseCancellationToken);
                if (writeResult.IsCanceled || writeResult.IsCompleted)
                    return (false, correlationId);
                writeResult =
                    await _networkStreamPipeWriter.WriteAsync(correlation.Memory, _baseCancellationToken);
                if (writeResult.IsCanceled || writeResult.IsCompleted)
                    return (false, correlationId);
                if (serializedBody.Length > 0)
                {
                    writeResult =
                        await _networkStreamPipeWriter.WriteAsync(serializedBody.Memory, _baseCancellationToken);

                    if (writeResult.IsCanceled || writeResult.IsCompleted)
                        return (false, correlationId);
                }

                return (true, correlationId);
            }
            catch (Exception e)
            {
                _logger?.LogError("{FunctionName} Got {ExceptionType}: {ExceptionMessage}", nameof(SendAsync),
                    e.GetType(), e.Message);
                throw;
            }
        }

        /// <summary>
        ///     Begins an asynchronous request to receive response associated with the specified ID from a
        ///     connected <see cref="StealthSharpClient" /> object.
        /// </summary>
        /// <param name="responseId"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="StealthSharpException"></exception>
        public async Task<TBody> ReceiveAsync<TBody>(ushort responseId,
            CancellationToken token = default)
        {
            if (_disposing)
                throw new ObjectDisposedException(nameof(_tcpClient));

            var serializationResult = await _completeResponses.WaitAsync(responseId, token);
            var response = _marshaler.Deserialize<TBody>(serializationResult);
            return response;
        }

        public void Dispose()
        {
            _logger?.LogInformation("Dispose started");
            if (_disposing)
                return;

            _disposing = true;

            if (!_baseCancellationTokenSource.IsCancellationRequested)
                _baseCancellationTokenSource.Cancel();

            _completeResponses.Dispose();

            _baseCancellationTokenSource.Dispose();
            _tcpClient.Dispose();
            _logger?.LogInformation("Dispose ended");

            GC.SuppressFinalize(this);
        }

        private void SetupTcpClient()
        {
            if (!_tcpClient.Connected)
                throw new SocketException(10057);

            _logger?.LogInformation("Connected to {Endpoint}", _tcpClient.Client.RemoteEndPoint as IPEndPoint);
            _tcpClient.SendTimeout = _options.TcpClientSendTimeout;
            _tcpClient.ReceiveTimeout = _options.TcpClientReceiveTimeout;
            _networkStreamPipeReader = PipeReader.Create(_tcpClient.GetStream(), _options.StreamPipeReaderOptions);
            _networkStreamPipeWriter = PipeWriter.Create(_tcpClient.GetStream(), _options.StreamPipeWriterOptions);
        }

        private void SetupTasks()
        {
            Task.Factory.StartNew(() => DeserializeResponseAsync().ContinueWith(_ =>
                {
                    if (_disposing)
                        return;

                    foreach (var kv in _completeResponses.ToArray())
                        kv.Value.TrySetException(StealthSharpException.ConnectionBroken());
                }, TaskContinuationOptions.OnlyOnRanToCompletion), _baseCancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        private async Task DeserializeResponseAsync()
        {
            try
            {
                while (true)
                {
                    _baseCancellationToken.ThrowIfCancellationRequested();

                    var lenReadResult =
                        await _networkStreamPipeReader.ReadDataAsync(6, _baseCancellationToken)
                            .ConfigureAwait(false);
                    using var serializationResult = new SerializationResult(lenReadResult.Slice(0, 6));
                    var header = _marshaler.Deserialize<PacketHeader>(serializationResult);
                    _networkStreamPipeReader.Consume(lenReadResult.Buffer.GetPosition(6));
                    if (header.Length <= 4)
                        continue;
                    var dataResult = await _networkStreamPipeReader.ReadDataAsync(header.Length - 2,
                        _baseCancellationToken).ConfigureAwait(false);
                    var sequence = dataResult.Slice(0, (int)header.Length - 2);

                    switch (header.PacketType)
                    {
                        case PacketType.SCExecEventProc:
                        {
                            var ev = _marshaler.Deserialize<ServerEventData>(new SerializationResult(sequence));
                            _observers
                                .AsParallel()
                                .ForEach(observer => observer.OnNext(ev));
                        }
                            break;
                        case PacketType.SCReturnValue:
                        {
                            var requestId =
                                _marshaler.Deserialize<ushort>(new SerializationResult(sequence.Slice(0, 2)));
                            await _completeResponses.SetAsync(requestId, new SerializationResult(sequence.Slice(2)),
                                true);
                        }
                            break;
                        case PacketType.SCScriptDLLTerminate:
                            break;
                        case PacketType.SCPauseResumeScript:
                            break;
                        case PacketType.SCErrorReport:
                        {
                            var requestId =
                                _marshaler.Deserialize<ushort>(new SerializationResult(sequence.Slice(0, 2)));
                            var errorCode =
                                _marshaler.Deserialize<ErrorCode>(new SerializationResult(sequence.Slice(2)));
                            _logger?.LogInformation("Error code {ErrorCode}. Terminate", errorCode);
                            _completeResponses[requestId]
                                .TrySetException(StealthSharpException.StealthError(errorCode));
                        }
                            break;
                    }

                    _networkStreamPipeReader.Consume(sequence.End);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                var exceptionType = exception.GetType();
                _logger?.LogCritical("DeserializeResponseAsync Got {ExceptionType}, {Message}", exceptionType,
                    exception.Message);
                throw;
            }
            finally
            {
                StopDeserializeWriterReader();
            }
        }

        private void StopDeserializeWriterReader()
        {
            if (!_baseCancellationTokenSource.IsCancellationRequested)
            {
                Debug.WriteLine("Cancelling _baseCancellationTokenSource from StopDeserializeWriterReader");
                _baseCancellationTokenSource.Cancel();
            }

            Debug.WriteLine("Completion Deserializer PipeWriter and PipeReader ended");
        }

        public IDisposable Subscribe(IObserver<ServerEventData> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer)) _observers.Add(observer);

            return new Unsubscriber<ServerEventData>(_observers, observer);
        }
    }
}