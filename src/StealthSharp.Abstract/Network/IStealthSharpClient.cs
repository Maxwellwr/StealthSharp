#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IStealthSharpClient.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.IO.Pipelines;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Event;

#endregion

namespace StealthSharp.Network
{
    public interface IStealthSharpClient : IDisposable, IDuplexPipe, IObservable<ServerEventData>
    {
        void Connect(IPAddress address, int port);

        /// <summary>
        ///     Serialize and sends data asynchronously to a connected <see cref="IStealthSharpClient" /> object.
        /// </summary>
        /// <param name="packetType"></param>
        /// <param name="body"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <exception cref="StealthSharpException"></exception>
        Task<(bool status, ushort correlationId)> SendAsync(PacketType packetType, object? body = null, CancellationToken token = default);

        /// <summary>
        ///     Begins an asynchronous request to receive response associated with the specified ID from a
        ///     connected <see cref="IStealthSharpClient" /> object.
        /// </summary>
        /// <param name="responseId"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="StealthSharpException"></exception>
        Task<TBody> ReceiveAsync<TBody>(ushort responseId, CancellationToken token = default);
    }
}