// using System;
// using System.Buffers;
// using System.Collections.Immutable;
// using System.Diagnostics;
// using System.IO.Pipelines;
// using System.Linq;
// using System.Net;
// using System.Net.Sockets;
// using System.Threading;
// using System.Threading.Tasks;
// using System.Threading.Tasks.Dataflow;
// using Drenalol.WaitingDictionary;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using Nito.AsyncEx;
// using StealthSharp.Serialization;
//
// namespace StealthSharp.Network
// {
//     public class StealthSharpClient<TId> : IStealthSharpClient
//     {
//         [DebuggerNonUserCode] private Guid Id { get; }
//
//         private readonly BatchRules<object> _batchRules;
//         private readonly CancellationTokenSource _baseCancellationTokenSource;
//         private readonly CancellationToken _baseCancellationToken;
//         private readonly TcpClient _tcpClient;
//         private readonly BufferBlock<ISerializationResult> _bufferBlockRequests;
//         private readonly WaitingDictionary<TId, IBatch<object>> _completeResponses;
//         private readonly IPacketSerializer _serializer;
//         private readonly IBitConvert _bitConvert;
//         private readonly AsyncManualResetEvent _writeResetEvent;
//         private readonly AsyncManualResetEvent _readResetEvent;
//         private readonly AsyncManualResetEvent _consumingResetEvent;
//         private readonly PipeReader _deserializePipeReader;
//         private readonly PipeWriter _deserializePipeWriter;
//         private readonly StealthSharpClientOptions _options;
//         private readonly ILogger<StealthSharpClient<TId>> _logger;
//         private Exception _internalException;
//         private PipeReader _networkStreamPipeReader;
//         private PipeWriter _networkStreamPipeWriter;
//         private bool _pipelineReadEnded;
//         private bool _pipelineWriteEnded;
//         private bool _disposing;
//         private long _bytesWrite;
//         private long _bytesRead;
//         PipeReader IDuplexPipe.Input => _networkStreamPipeReader;
//         PipeWriter IDuplexPipe.Output => _networkStreamPipeWriter;
//
//         /// <summary>
//         /// Gets the number of total bytes written to the <see cref="NetworkStream"/>.
//         /// </summary>
//         public long BytesWrite => _bytesWrite;
//
//         /// <summary>
//         /// Gets the number of total bytes read from the <see cref="NetworkStream"/>.
//         /// </summary>
//         public long BytesRead => _bytesRead;
//
//         /// <summary>
//         /// Gets the number of responses to receive or the number of responses ready to receive.
//         /// </summary>
//         public int Waiters => _completeResponses.Count;
//
//         /// <summary>
//         /// Gets the number of requests ready to send.
//         /// </summary>
//         public int Requests => _bufferBlockRequests.Count;
//
//         /// <summary>
//         /// Gets an immutable snapshot of responses to receive (id, null) or responses ready to receive (id, <see cref="object"/>).
//         /// </summary>
//         public ImmutableDictionary<TId, WaiterInfo<IBatch<object>>> GetWaiters()
//             => _completeResponses
//                 .ToImmutableDictionary(pair => pair.Key, pair => new WaiterInfo<IBatch<object>>(pair.Value.Task));
//
//         public void Connect(IPAddress address, int port)
//         {
//             _tcpClient.Connect(address, port);
//             SetupTcpClient();
//             SetupTasks();
//         }
//
//         public StealthSharpClient(
//             IPacketSerializer serializer,
//             IBitConvert bitConvert,
//             IOptions<StealthSharpClientOptions> options,
//             ILogger<StealthSharpClient<TId>> logger)
//         {
//             var pipe = new Pipe();
//             Id = Guid.NewGuid();
//             _tcpClient = new TcpClient();
//             _logger = logger;
//             _options = options?.Value ?? StealthSharpClientOptions.Default;
//             _baseCancellationTokenSource = new CancellationTokenSource();
//             _baseCancellationToken = _baseCancellationTokenSource.Token;
//             _bufferBlockRequests = new BufferBlock<ISerializationResult>();
//             _bitConvert = bitConvert;
//             var middleware = new MiddlewareBuilder<IBatch<object>>()
//                 .RegisterCancellationActionInWait((tcs, hasOwnToken) =>
//                 {
//                     if (_disposing || hasOwnToken)
//                         tcs.TrySetCanceled();
//                     else if (!_disposing && _pipelineReadEnded)
//                         tcs.TrySetException(StealthSharpClientException.ConnectionBroken());
//                 })
//                 .RegisterDuplicateActionInSet((batch, newBatch) => _batchRules.Update(batch, newBatch.Single()))
//                 .RegisterCompletionActionInSet(() => _consumingResetEvent.Set());
//             _completeResponses = new WaitingDictionary<TId, IBatch<object>>(middleware);
//             _serializer = serializer;
//             _writeResetEvent = new AsyncManualResetEvent();
//             _readResetEvent = new AsyncManualResetEvent();
//             _consumingResetEvent = new AsyncManualResetEvent();
//             _deserializePipeReader = pipe.Reader;
//             _deserializePipeWriter = pipe.Writer;
//         }
//
//         /// <summary>
//         /// Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient{TId}"/> object.
//         /// </summary>
//         /// <param name="request"></param>
//         /// <param name="token"></param>
//         /// <returns><see cref="bool"/></returns>
//         /// <exception cref="StealthSharpClientException"></exception>
//         public async Task<bool> SendAsync<TRequest>(TRequest request, CancellationToken token = default)
//         {
//             try
//             {
//                 if (_disposing)
//                     throw new ObjectDisposedException(nameof(_tcpClient));
//
//                 if (!_disposing && _pipelineWriteEnded)
//                     throw StealthSharpClientException.ConnectionBroken();
//
//                 var serializationResult = _serializer.Serialize(request);
//                 return await _bufferBlockRequests.SendAsync(serializationResult,
//                     token == default ? _baseCancellationToken : token);
//             }
//             catch (Exception e)
//             {
//                 _logger?.LogError($"{nameof(SendAsync)} Got {e.GetType()}: {e.Message}");
//                 throw;
//             }
//         }
//
//         private void SetupTcpClient()
//         {
//             if (!_tcpClient.Connected)
//                 throw new SocketException(10057);
//
//             _logger?.LogInformation($"Connected to {(IPEndPoint) _tcpClient.Client.RemoteEndPoint}");
//             _tcpClient.SendTimeout = _options.TcpClientSendTimeout;
//             _tcpClient.ReceiveTimeout = _options.TcpClientReceiveTimeout;
//             _networkStreamPipeReader = PipeReader.Create(_tcpClient.GetStream(), _options.StreamPipeReaderOptions);
//             _networkStreamPipeWriter = PipeWriter.Create(_tcpClient.GetStream(), _options.StreamPipeWriterOptions);
//         }
//
//         private void SetupTasks()
//         {
//             _ = TcpWriteAsync();
//             _ = TcpReadAsync().ContinueWith(antecedent =>
//             {
//                 if (_disposing || !_pipelineReadEnded)
//                     return;
//
//                 foreach (var kv in _completeResponses.ToArray())
//                     kv.Value.TrySetException(StealthSharpClientException.ConnectionBroken());
//             }, TaskContinuationOptions.OnlyOnRanToCompletion);
//             _ = DeserializeResponseAsync<>()
//         }
//
//         public async ValueTask DisposeAsync()
//         {
//             _logger?.LogInformation("Dispose started");
//             _disposing = true;
//
//             if (_baseCancellationTokenSource != null && !_baseCancellationTokenSource.IsCancellationRequested)
//                 _baseCancellationTokenSource.Cancel();
//
//             _completeResponses?.Dispose();
//
//             using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
//             {
//                 var token = cts.Token;
//                 await _writeResetEvent.WaitAsync(token);
//                 await _readResetEvent.WaitAsync(token);
//             }
//
//             _baseCancellationTokenSource?.Dispose();
//             _tcpClient?.Dispose();
//             _logger?.LogInformation("Dispose ended");
//         }
//
//         private async Task TcpWriteAsync()
//         {
//             try
//             {
//                 while (true)
//                 {
//                     _baseCancellationToken.ThrowIfCancellationRequested();
//
//                     if (!await _bufferBlockRequests.OutputAvailableAsync(_baseCancellationToken))
//                         continue;
//
//                     using var serializedRequest = await _bufferBlockRequests.ReceiveAsync(_baseCancellationToken);
//                     var writeResult =
//                         await _networkStreamPipeWriter.WriteAsync(serializedRequest.Memory, _baseCancellationToken);
//
//                     if (writeResult.IsCanceled || writeResult.IsCompleted)
//                         break;
//
//                     Interlocked.Add(ref _bytesWrite, serializedRequest.Memory.Length);
//                 }
//             }
//             catch (OperationCanceledException canceledException)
//             {
//                 _internalException = canceledException;
//             }
//             catch (Exception exception)
//             {
//                 var exceptionType = exception.GetType();
//                 _logger?.LogCritical("TcpWriteAsync Got {ExceptionType}, {Message}", exceptionType, exception.Message);
//                 _internalException = exception;
//                 throw;
//             }
//             finally
//             {
//                 _pipelineWriteEnded = true;
//                 StopWriter(_internalException);
//                 _writeResetEvent.Set();
//             }
//         }
//
//         private async Task TcpReadAsync()
//         {
//             try
//             {
//                 while (true)
//                 {
//                     _baseCancellationToken.ThrowIfCancellationRequested();
//                     var readResult = await _networkStreamPipeReader.ReadAsync(_baseCancellationToken);
//
//                     if (readResult.IsCanceled || readResult.IsCompleted)
//                         break;
//
//                     if (readResult.Buffer.IsEmpty)
//                         continue;
//
//                     foreach (var buffer in readResult.Buffer)
//                     {
//                         await _deserializePipeWriter.WriteAsync(buffer, _baseCancellationToken);
//                         Interlocked.Add(ref _bytesRead, buffer.Length);
//                     }
//
//                     _networkStreamPipeReader.AdvanceTo(readResult.Buffer.End);
//                 }
//             }
//             catch (OperationCanceledException canceledException)
//             {
//                 _internalException = canceledException;
//             }
//             catch (Exception exception)
//             {
//                 var exceptionType = exception.GetType();
//                 _logger?.LogCritical("TcpReadAsync Got {ExceptionType}, {Message}", exceptionType, exception.Message);
//                 _internalException = exception;
//                 throw;
//             }
//             finally
//             {
//                 _pipelineReadEnded = true;
//                 StopReader(_internalException);
//                 _readResetEvent.Set();
//             }
//         }
//
//         /// <summary>
//         /// Begins an asynchronous request to receive response associated with the specified ID from a connected <see cref="TcpClientIo{TId,TRequest,TResponse}"/> object.
//         /// </summary>
//         /// <param name="responseId"></param>
//         /// <param name="token"></param>
//         /// <returns><see cref="ITcpBatch{TResponse}"/></returns>
//         /// <exception cref="TcpClientIoException"></exception>
//         public async Task<IBatch<TResponse>> ReceiveAsync<TResponse>(TId responseId, CancellationToken token = default)
//         {
//             if (_disposing)
//                 throw new ObjectDisposedException(nameof(_tcpClient));
//
//             if (!_disposing && _pipelineReadEnded)
//                 throw TcpClientIoException.ConnectionBroken();
//
//             return await _completeResponses.WaitAsync(responseId, token);
//         }
//         
//         public async Task<T> DeserializeResponseAsync<T>() where T: ICorrelation<TId>
//         {
//             try
//             {
//                 while (true)
//                 {
//                     _baseCancellationToken.ThrowIfCancellationRequested();
//                     var lenResult =
//                         await _deserializePipeReader.ReadLengthAsync(_serializer.LengthSize, _baseCancellationToken);
//                     var sequence = lenResult.Slice(_serializer.LengthSize);
//                     _bitConvert.ConvertFromBytes(out var size, _bitConvert.LengthType, sequence.ToArray());
//                     var dataSize = Convert.ToInt32(size);
//                     
//                     var dataResult = await _deserializePipeReader.ReadLengthAsync(_serializer.LengthSize + dataSize, _baseCancellationToken);
//                     sequence = dataResult.Slice(_serializer.LengthSize + dataSize);
//                     var result = new SerializationResult(_serializer.LengthSize + dataSize);
//                     sequence.CopyTo(result.Memory.Span);
//                     var response = _serializer.Deserialize<T>(result);
//                     await _completeResponses.SetAsync(response.CorellationId, _batchRules.Create(response), true);
//                     _deserializePipeReader.Consume(sequence.GetPosition(_serializer.LengthSize + dataSize));
//                     
//                     return  await 
//                 }
//             }
//             catch (OperationCanceledException canceledException)
//             {
//                 _internalException = canceledException;
//             }
//             catch (Exception exception)
//             {
//                 var exceptionType = exception.GetType();
//                 _logger?.LogCritical("DeserializeResponseAsync Got {ExceptionType}, {Message}", exceptionType,
//                     exception.Message);
//                 _internalException = exception;
//                 throw;
//             }
//             finally
//             {
//                 StopDeserializeWriterReader(_internalException);
//             }
//         }
//
//         private void StopDeserializeWriterReader(Exception exception)
//         {
//             Debug.WriteLine("Completion Deserializer PipeWriter and PipeReader started");
//             _deserializePipeWriter.CancelPendingFlush();
//             _deserializePipeReader.CancelPendingRead();
//
//             if (_tcpClient.Client.Connected)
//             {
//                 _deserializePipeWriter.Complete(exception);
//                 _deserializePipeReader.Complete(exception);
//             }
//
//             if (!_baseCancellationTokenSource.IsCancellationRequested)
//             {
//                 Debug.WriteLine("Cancelling _baseCancellationTokenSource from StopDeserializeWriterReader");
//                 _baseCancellationTokenSource.Cancel();
//             }
//
//             Debug.WriteLine("Completion Deserializer PipeWriter and PipeReader ended");
//         }
//
//         private void StopReader(Exception exception)
//         {
//             Debug.WriteLine("Completion NetworkStream PipeReader started");
//
//             if (_disposing)
//             {
//                 foreach (var completedResponse in _completeResponses.Where(tcs =>
//                     tcs.Value.Task.Status == TaskStatus.WaitingForActivation))
//                 {
//                     var innerException = exception ?? new OperationCanceledException();
//                     Debug.WriteLine(
//                         $"Set force {innerException.GetType()} in {nameof(TaskCompletionSource<ITcpBatch<TResponse>>)} in {nameof(TaskStatus.WaitingForActivation)}");
//                     completedResponse.Value.TrySetException(innerException);
//                 }
//             }
//
//             _networkStreamPipeReader.CancelPendingRead();
//
//             if (_tcpClient.Client.Connected)
//                 _networkStreamPipeReader.Complete(exception);
//
//             if (!_baseCancellationTokenSource.IsCancellationRequested)
//             {
//                 Debug.WriteLine("Cancelling _baseCancellationTokenSource from StopReader");
//                 _baseCancellationTokenSource.Cancel();
//             }
//
//             Debug.WriteLine("Completion NetworkStream PipeReader ended");
//         }
//
//         private void StopWriter(Exception exception)
//         {
//             Debug.WriteLine("Completion NetworkStream PipeWriter started");
//             _networkStreamPipeWriter.CancelPendingFlush();
//
//             if (_tcpClient.Client.Connected)
//                 _networkStreamPipeWriter.Complete(exception);
//
//             _bufferBlockRequests.Complete();
//
//             if (!_baseCancellationTokenSource.IsCancellationRequested)
//             {
//                 Debug.WriteLine("Cancelling _baseCancellationTokenSource from StopWriter");
//                 _baseCancellationTokenSource.Cancel();
//             }
//
//             Debug.WriteLine("Completion NetworkStream PipeWriter ended");
//         }
//     }
// }