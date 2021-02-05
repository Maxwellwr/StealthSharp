#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClient.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Drenalol.WaitingDictionary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using StealthSharp.Serialization;

namespace StealthSharp.Network
{
    public class StealthSharpClient<TId, TSize, TMapping> : IStealthSharpClient<TId, TSize, TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPacketCorrelationGenerator<TId> _correlationGenerator;
        private readonly ITypeMapper<TMapping, TId> _typeMapper;
        private readonly CancellationTokenSource _baseCancellationTokenSource;
        private readonly CancellationToken _baseCancellationToken;
        private readonly TcpClient _tcpClient;
        private readonly WaitingDictionary<TId, object> _completeResponses;
        private readonly IPacketSerializer _serializer;
        private readonly IReflectionCache _reflectionCache;
        private readonly StealthSharpClientOptions _options;
        private readonly ILogger<StealthSharpClient<TId, TSize, TMapping>>? _logger;
        private Exception? _internalException;
        private PipeReader _networkStreamPipeReader;
        private PipeWriter _networkStreamPipeWriter;
        private bool _disposing;

        PipeReader IDuplexPipe.Input =>
            _networkStreamPipeReader;

        PipeWriter IDuplexPipe.Output =>
            _networkStreamPipeWriter;

        public ITypeMapper<TMapping, TId> TypeMapper =>
            _typeMapper;

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
            ITypeMapper<TMapping, TId> typeMapper,
            IPacketCorrelationGenerator<TId> correlationGenerator,
            IOptions<StealthSharpClientOptions>? options,
            ILogger<StealthSharpClient<TId, TSize, TMapping>>? logger)
        {
            var pipe = new Pipe();
            _serviceProvider = serviceProvider;
            _correlationGenerator = correlationGenerator;
            _typeMapper = typeMapper;
            _tcpClient = new TcpClient();
            _logger = logger;
            _options = options?.Value ?? StealthSharpClientOptions.Default;
            _baseCancellationTokenSource = new CancellationTokenSource();
            _baseCancellationToken = _baseCancellationTokenSource.Token;
            _reflectionCache = reflectionCache;
            var middleware = new MiddlewareBuilder<object>()
                .RegisterCancellationActionInWait((tcs, hasOwnToken) =>
                {
                    if (_disposing || hasOwnToken)
                        tcs.TrySetCanceled();
                    else if (!_disposing)
                        tcs.TrySetException(StealthSharpClientException.ConnectionBroken());
                });
            _completeResponses = new WaitingDictionary<TId, object>(middleware);
            _serializer = serializer;
            _networkStreamPipeReader = pipe.Reader;
            _networkStreamPipeWriter = pipe.Writer;
        }


        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient{TId, TSize, TMapping}" />
        ///     object.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharpClientException"></exception>
        public TId Send(IPacket<TId, TSize, TMapping> request)
        {
            try
            {
                if (_disposing)
                    throw new ObjectDisposedException(nameof(_tcpClient));

                if (EqualityComparer<TId>.Default.Equals(request.CorrelationId, default))
                    request.CorrelationId = _correlationGenerator.GetNextCorrelationId();
                var serializedRequest = _serializer.Serialize(request);

                _ = _networkStreamPipeWriter.WriteAsync(serializedRequest.Memory, _baseCancellationToken).ConfigureAwait(false);

                return request.CorrelationId;
            }
            catch (Exception e)
            {
                _logger?.LogError("{FunctionName} Got {ExceptionType}: {ExceptionMessage}", nameof(Send),
                    e.GetType(), e.Message);
                throw;
            }
        }

        public TId Send<TResponse>(IPacket<TId, TSize, TMapping> request)
        {
            try
            {
                var correlationId = Send(request);
                _ = _typeMapper.SetMappedTypeAsync(correlationId, typeof(TResponse));
                return correlationId;
            }
            catch (Exception e)
            {
                _logger?.LogError("{FunctionName} Got {ExceptionType}: {ExceptionMessage}", nameof(Send),
                    e.GetType(), e.Message);
                throw;
            }
        }
        /// <summary>
        ///     Begins an asynchronous request to receive response associated with the specified ID from a
        ///     connected <see cref="StealthSharpClient{TId, TSize, TMapping}" /> object.
        /// </summary>
        /// <param name="responseId"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="IPacket{TId, TSize, TMapping, TBody}" />
        /// </returns>
        /// <exception cref="StealthSharpClientException"></exception>
        public async Task<IPacket<TId, TSize, TMapping, TBody>> ReceiveAsync<TBody>(TId responseId,
            CancellationToken token = default)
        {
            if (_disposing)
                throw new ObjectDisposedException(nameof(_tcpClient));

            var p = await _completeResponses.WaitAsync(responseId, token).ConfigureAwait(false);
            return (IPacket<TId, TSize, TMapping, TBody>) p;
        }

        /// <summary>
        ///     Begins an asynchronous request to receive response associated with the specified ID from a
        ///     connected <see cref="StealthSharpClient{TId, TSize, TMapping}" /> object.
        /// </summary>
        /// <param name="responseId"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="IPacket{TId, TSize, TMapping, TBody}" />
        /// </returns>
        /// <exception cref="StealthSharpClientException"></exception>
        public async Task<IPacket<TId, TSize, TMapping>> ReceiveAsync(TId responseId, CancellationToken token = default)
        {
            if (_disposing)
                throw new ObjectDisposedException(nameof(_tcpClient));

            return (IPacket<TId, TSize, TMapping>) (await _completeResponses.WaitAsync(responseId, token).ConfigureAwait(false));
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

                    var metaType = _serviceProvider.GetRequiredService(CreateGeneric(null));

                    var metadata = _reflectionCache.GetMetadata(metaType.GetType());
                    if (metadata == null)
                        throw new NullReferenceException();

                    var lengthStart = metadata[PacketDataType.Length].Attribute.Index;
                    var lenEnd = metadata[PacketDataType.Length].Attribute.Index + metadata.LengthLength;

                    var lenResult =
                        await _networkStreamPipeReader.ReadLengthAsync(lenEnd, _baseCancellationToken).ConfigureAwait(false);
                    var sequence = lenResult.Slice(lengthStart, metadata.LengthLength);
                    _serializer.Deserialize(sequence.ToArray(),
                        metadata[PacketDataType.Length].PropertyType,
                        out var size);
                    var dataSize = Convert.ToInt32(size);

                    _networkStreamPipeReader.Examine(sequence.Start, sequence.End);

                    var dataResult = await _networkStreamPipeReader.ReadLengthAsync(metadata.LengthLength + dataSize,
                        _baseCancellationToken).ConfigureAwait(false);

                    var typeMapperStart = metadata[PacketDataType.TypeMapper].Attribute.Index;
                    sequence = dataResult.Slice(typeMapperStart, metadata.TypeMapperLength);
                    _serializer.Deserialize(sequence.ToArray(),
                        metadata[PacketDataType.TypeMapper].PropertyType,
                        out var typeId);

                    var correlationStart = metadata[PacketDataType.Id].Attribute.Index;
                    sequence = dataResult.Slice(correlationStart, metadata.IdLength);
                    _serializer.Deserialize(sequence.ToArray(),
                        metadata[PacketDataType.Id].PropertyType,
                        out var correlationId);

                    sequence = dataResult.Slice(0, metadata.LengthLength + dataSize);
                    var result = new SerializationResult(metadata.LengthLength + dataSize);
                    sequence.CopyTo(result.Memory.Span);
                    var realType =
                        _serviceProvider.GetRequiredService(
                            CreateGeneric(await _typeMapper.GetMappedTypeAsync((TMapping) typeId,
                                (TId) correlationId).ConfigureAwait(false)));
                    var response = _serializer.Deserialize(result, realType.GetType());
                    await _completeResponses.SetAsync((TId) correlationId, response, true).ConfigureAwait(false);
                    _networkStreamPipeReader.Consume(sequence.End);
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

        private Type CreateGeneric(Type? body)
        {
            if (body is null)
                return typeof(IPacket<,,>).MakeGenericType(typeof(TId), typeof(TSize), typeof(TMapping));
            return typeof(IPacket<,,,>).MakeGenericType(typeof(TId), typeof(TSize), typeof(TMapping), body);
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