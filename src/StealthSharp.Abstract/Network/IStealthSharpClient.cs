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
using StealthSharp.Enum;

namespace StealthSharp.Network
{
    public interface IStealthSharpClient : IDisposable, IDuplexPipe
    {
        void Connect(IPAddress address, int port);

        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="StealthSharpClient" /> object.
        /// </summary>
        /// <param name="packetType"></param>
        /// <param name="body"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharp.Network.StealthSharpClientException"></exception>
        Task<(bool status, ushort correlationId)> SendAsync(PacketType packetType,
            object? body = null,
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
        Task<TBody> ReceiveAsync<TBody>(ushort responseId,
            CancellationToken token = default);
    }
}