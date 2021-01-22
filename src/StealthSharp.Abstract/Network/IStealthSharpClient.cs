using System;
using System.IO.Pipelines;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StealthSharp.Network
{
    public interface IStealthSharpClient:IAsyncDisposable, IDuplexPipe
    {
        void Connect(IPAddress address, int port);

        /// <summary>
        /// Serialize and sends data asynchronously to a connected <see cref="TcpClientIo{TRequest,TResponse}"/> object.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns><see cref="bool"/></returns>
        /// <exception cref="StealthSharp.Network.StealthSharpClientException"></exception>
        Task<bool> SendAsync<TRequest>(TRequest request, CancellationToken token = default);
    }
}