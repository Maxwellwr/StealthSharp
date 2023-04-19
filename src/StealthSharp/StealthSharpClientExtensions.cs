#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

#endregion

namespace StealthSharp
{
    public static class StealthSharpClientExtensions
    {
        public static Task<TResult> SendPacketAsync<TResult>(
            this IStealthSharpClient client,
            PacketType packetType,
            CancellationToken cancellationToken = default)
        {
            return SendPacketAsync<object, TResult>(client, packetType, null, cancellationToken);
        }

        public static async Task<TResult> SendPacketAsync<TBody, TResult>(
            this IStealthSharpClient client,
            PacketType packetType,
            TBody? body,
            CancellationToken cancellationToken = default)
        {
            var (status, correlationId) = await client.SendAsync(packetType, body, cancellationToken).ConfigureAwait(false);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
            var recv = await client.ReceiveAsync<TResult>(correlationId, cancellationToken).ConfigureAwait(false);
            return recv;
        }

        public static async Task SendPacketAsync<TBody>(
            this IStealthSharpClient client,
            PacketType packetType,
            TBody? body,
            CancellationToken cancellationToken = default)
        {
            var (status, _) = await client.SendAsync(packetType, body, cancellationToken).ConfigureAwait(false);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
        }

        public static Task SendPacketAsync(
            this IStealthSharpClient client,
            PacketType packetType,
            CancellationToken cancellationToken = default)
        {
            return SendPacketAsync<object>(client, packetType, null, cancellationToken);
        }
    }
}