using System.Collections.Generic;

namespace StealthSharp.Network
{
    /// <summary>
    /// Batch of responses.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns><see cref="ITcpBatch{TResponse}"/></returns>
    public interface IBatch<TResponse> : IEnumerable<TResponse>
    {
        int Count { get; }
        void Add(TResponse response);
    }
}