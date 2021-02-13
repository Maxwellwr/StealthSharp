#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClient.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
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
using StealthSharp.Enum;
using StealthSharp.Serialization;

namespace StealthSharp.Network
{
    public class StealthSharpClient : IStealthSharpClient
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPacketCorrelationGenerator<ushort> _correlationGenerator;
        private readonly CancellationTokenSource _baseCancellationTokenSource;
        private readonly CancellationToken _baseCancellationToken;
        private readonly TcpClient _tcpClient;
        private readonly WaitingDictionary<ushort, SerializationResult> _completeResponses;
        private readonly IPacketSerializer _serializer;
        private readonly IReflectionCache _reflectionCache;
        private readonly StealthSharpClientOptions _options;
        private readonly ILogger<StealthSharpClient>? _logger;
        private Exception? _internalException;
        private PipeReader _networkStreamPipeReader;
        private PipeWriter _networkStreamPipeWriter;
        private bool _disposing;

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
            IPacketSerializer serializer,
            IReflectionCache reflectionCache,
            IServiceProvider serviceProvider,
            IPacketCorrelationGenerator<ushort> correlationGenerator,
            IOptions<StealthSharpClientOptions>? options,
            ILogger<StealthSharpClient>? logger)
        {
            var pipe = new Pipe();
            _serviceProvider = serviceProvider;
            _correlationGenerator = correlationGenerator;
            _tcpClient = new TcpClient();
            _logger = logger;
            _options = options?.Value ?? StealthSharpClientOptions.Default;
            _baseCancellationTokenSource = new CancellationTokenSource();
            _baseCancellationToken = _baseCancellationTokenSource.Token;
            _reflectionCache = reflectionCache;
            var middleware = new MiddlewareBuilder<SerializationResult>()
                .RegisterCancellationActionInWait((tcs, hasOwnToken) =>
                {
                    if (_disposing || hasOwnToken)
                        tcs.TrySetCanceled();
                    else if (!_disposing)
                        tcs.TrySetException(StealthSharpClientException.ConnectionBroken());
                });
            _completeResponses = new WaitingDictionary<ushort, SerializationResult>(middleware);
            _serializer = serializer;
            _networkStreamPipeReader = pipe.Reader;
            _networkStreamPipeWriter = pipe.Writer;
        }


        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient{ushort, uint, PacketType}" />
        ///     object.
        /// </summary>
        /// <param name="packetType"></param>
        /// <param name="body"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharpClientException"></exception>
        public async Task<(bool status, ushort correlationId)> SendAsync(PacketType packetType,
            object? body = null,
            CancellationToken token = default)
        {
            try
            {
                if (_disposing)
                    throw new ObjectDisposedException(nameof(_tcpClient));
                
                using var serializedBody = body is not null ? 
                    _serializer.Serialize(body) : 
                    new SerializationResult(0);

                var packetHeader = new PacketHeader
                {
                    CorrelationId = _correlationGenerator.GetNextCorrelationId(),
                    PacketType = packetType,
                    Length = (uint)(serializedBody.Memory.Length + 4)
                };
                using var serializedHeader = _serializer.Serialize(packetHeader);

                var writeResult =
                    await _networkStreamPipeWriter.WriteAsync(serializedHeader.Memory, _baseCancellationToken).ConfigureAwait(false);
                if (writeResult.IsCanceled || writeResult.IsCompleted)
                    return (false, packetHeader.CorrelationId);
                if (serializedBody.Length > 0)
                {
                    writeResult =
                        await _networkStreamPipeWriter.WriteAsync(serializedBody.Memory, _baseCancellationToken)
                            .ConfigureAwait(false);

                    if (writeResult.IsCanceled || writeResult.IsCompleted)
                        return (false, packetHeader.CorrelationId);
                }

                return (true, packetHeader.CorrelationId);
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
        ///     connected <see cref="StealthSharpClient{ushort, uint, PacketType}" /> object.
        /// </summary>
        /// <param name="responseId"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="IPacket{ushort, uint, PacketType, TBody}" />
        /// </returns>
        /// <exception cref="StealthSharpClientException"></exception>
        public async Task<TBody> ReceiveAsync<TBody>(ushort responseId,
            CancellationToken token = default)
        {
            if (_disposing)
                throw new ObjectDisposedException(nameof(_tcpClient));

            var serializationResult = await _completeResponses.WaitAsync(responseId, token).ConfigureAwait(false);
            var response = _serializer.Deserialize<TBody>(serializationResult);
            return response;
        }

        public void Dispose()
        {
            _logger?.LogInformation("Dispose started");
            _disposing = true;

            if (!_baseCancellationTokenSource.IsCancellationRequested)
                _baseCancellationTokenSource.Cancel();

            _completeResponses?.Dispose();

            _baseCancellationTokenSource?.Dispose();
            _tcpClient?.Dispose();
            _logger?.LogInformation("Dispose ended");
        }

        private void SetupTcpClient()
        {
            if (!_tcpClient.Connected)
                throw new SocketException(10057);

            _logger?.LogInformation($"Connected to {(IPEndPoint) _tcpClient.Client.RemoteEndPoint}");
            _tcpClient.SendTimeout = _options.TcpClientSendTimeout;
            _tcpClient.ReceiveTimeout = _options.TcpClientReceiveTimeout;
            _networkStreamPipeReader = PipeReader.Create(_tcpClient.GetStream(), _options.StreamPipeReaderOptions);
            _networkStreamPipeWriter = PipeWriter.Create(_tcpClient.GetStream(), _options.StreamPipeWriterOptions);
        }

        private void SetupTasks()
        {
            _ = DeserializeResponseAsync().ContinueWith(antecedent =>
            {
                if (_disposing)
                    return;

                foreach (var kv in _completeResponses.ToArray())
                    kv.Value.TrySetException(StealthSharpClientException.ConnectionBroken());
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private async Task DeserializeResponseAsync()
        {
            try
            {
                while (true)
                {
                    _baseCancellationToken.ThrowIfCancellationRequested();

                    var lenReadResult =
                        await _networkStreamPipeReader.ReadDataAsync(8, _baseCancellationToken)
                            .ConfigureAwait(false);
                    using var serializationResult = new SerializationResult(lenReadResult.Slice(0,8));
                    var header = _serializer.Deserialize<PacketHeader>(serializationResult);
                    _networkStreamPipeReader.Consume(lenReadResult.Buffer.GetPosition(8));
                    if (header.Length > 4)
                    {
                        var dataResult = await _networkStreamPipeReader.ReadDataAsync(header.Length - 4,
                            _baseCancellationToken).ConfigureAwait(false);
                        var sequence = dataResult.Slice(0, (int) header.Length - 4);
                        var result = new SerializationResult(sequence);
                        
                        await _completeResponses.SetAsync(header.CorrelationId, result, true).ConfigureAwait(false);
                        
                        _networkStreamPipeReader.Consume(sequence.End);
                    }
                }
            }
            catch (OperationCanceledException canceledException)
            {
                _internalException = canceledException;
            }
            catch (Exception exception)
            {
                var exceptionType = exception.GetType();
                _logger?.LogCritical("DeserializeResponseAsync Got {ExceptionType}, {Message}", exceptionType,
                    exception.Message);
                _internalException = exception;
                throw;
            }
            finally
            {
                StopDeserializeWriterReader(_internalException);
            }
        }

        private void StopDeserializeWriterReader(Exception exception)
        {
            if (!_baseCancellationTokenSource.IsCancellationRequested)
            {
                Debug.WriteLine("Cancelling _baseCancellationTokenSource from StopDeserializeWriterReader");
                _baseCancellationTokenSource.Cancel();
            }

            Debug.WriteLine("Completion Deserializer PipeWriter and PipeReader ended");
        }
    }
}