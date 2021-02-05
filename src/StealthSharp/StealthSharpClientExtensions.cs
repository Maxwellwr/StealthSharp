#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp
{
    public static class StealthSharpClientExtensions
    {
        public static async Task<TResult> SendPacketAsync<TResult>(this IStealthSharpClient<ushort, uint, ushort> client,
            PacketType packetType, CancellationToken cancellationToken = default)
        {
            var packet = new Packet<ushort, uint, ushort>()
            {
                TypeId = (ushort) packetType
            };
            var recv = await client.ReceiveAsync<TResult>(client.Send<TResult>(packet), cancellationToken);
            return recv.Body;
        }

        public static async Task<TResult> SendPacketAsync<TBody, TResult>(
            this IStealthSharpClient<ushort, uint, ushort> client, PacketType packetType, TBody body,
            CancellationToken cancellationToken = default)
        {
            var packet = new Packet<ushort, uint, ushort, TBody>()
            {
                TypeId = (ushort) packetType,
                Body = body
            };
            var recv = await client.ReceiveAsync<TResult>(client.Send<TResult>(packet), cancellationToken);
            return recv.Body;
        }

        public static void SendPacket<TBody>(this IStealthSharpClient<ushort, uint, ushort> client,
            PacketType packetType, TBody body)
        {
            var packet = new Packet<ushort, uint, ushort, TBody>()
            {
                TypeId = (ushort) packetType,
                Body = body
            };
            _ = client.Send(packet);
        }

        public static void SendPacket(this IStealthSharpClient<ushort, uint, ushort> client,
            PacketType packetType)
        {
            var packet = new Packet<ushort, uint, ushort>()
            {
                TypeId = (ushort) packetType
            };
            _ = client.Send(packet);
        }
    }
}