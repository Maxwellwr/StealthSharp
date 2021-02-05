#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IStealthSharpClient.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.IO.Pipelines;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StealthSharp.Network
{
    public interface IStealthSharpClient<TId, TSize, TMapping> : IDisposable, IDuplexPipe
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        void Connect(IPAddress address, int port);

        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="TcpClientIo{TRequest,TResponse}" /> object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharp.Network.StealthSharpClientException"></exception>
        TId Send<TBody>(IPacket<TId, TSize, TMapping, TBody> request) =>
            Send((IPacket<TId, TSize, TMapping>) request);

        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="TcpClientIo{TRequest,TResponse}" /> object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharp.Network.StealthSharpClientException"></exception>
        TId Send<TBody, TResult>(IPacket<TId, TSize, TMapping, TBody> request) =>
            Send<TResult>((IPacket<TId, TSize, TMapping>) request);

        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient" /> object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharp.Network.StealthSharpClientException"></exception>
        TId Send(IPacket<TId, TSize, TMapping> request);

        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient" /> object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharp.Network.StealthSharpClientException"></exception>
        TId Send<TResult>(IPacket<TId, TSize, TMapping> request);

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
        Task<IPacket<TId, TSize, TMapping, TBody>> ReceiveAsync<TBody>(TId responseId,
            CancellationToken token = default);

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
        Task<IPacket<TId, TSize, TMapping>> ReceiveAsync(TId responseId, CancellationToken token = default);
    }
}