#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp
{
    public static class StealthSharpClientExtensions
    {
        public static Task<TResult> SendPacketAsync<TResult>(this IStealthSharpClient client,
            PacketType packetType)
            => SendPacketAsync<object, TResult>(client, packetType, null);
        
        public static async Task<TResult> SendPacketAsync<TBody, TResult>(this IStealthSharpClient client, PacketType packetType, TBody? body)
        {
            var (status, correlationId) = await client.SendAsync(packetType, body);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
            var recv = await client.ReceiveAsync<TResult>(correlationId);
            return recv;
        }
        
        public static async Task SendPacketAsync<TBody>(this IStealthSharpClient client, PacketType packetType, TBody? body)
        {
            var (status, _) = await client.SendAsync(packetType, body);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
        }

        public static Task SendPacketAsync(this IStealthSharpClient client, PacketType packetType)
            => SendPacketAsync<object>(client, packetType, null);
    }
}