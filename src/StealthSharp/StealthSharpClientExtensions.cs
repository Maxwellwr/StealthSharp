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
        public static async Task<TResult> SendPacketAsync<TResult>(this IStealthSharpClient<ushort, uint, ushort> client, PacketType packetType)
        {
            var packet = new Packet<ushort, uint, ushort>()
            {
                TypeId = (ushort)packetType
            };
            var (status, correlationId) = await client.SendAsync<TResult>(packet);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
            var recv = await client.ReceiveAsync<TResult>(correlationId);
            return recv.Body;
        }
        
        public static async Task<TResult> SendPacketAsync<TBody, TResult>(this IStealthSharpClient<ushort, uint, ushort> client, PacketType packetType, TBody body)
        {
            var packet = new Packet<ushort, uint, ushort, TBody>()
            {
                TypeId = (ushort)packetType,
                Body = body
            };
            var (status, correlationId) = await client.SendAsync<TResult>(packet);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
            var recv = await client.ReceiveAsync<TResult>(correlationId);
            return recv.Body;
        }
        
        public static async Task SendPacketAsync<TBody>(this IStealthSharpClient<ushort, uint, ushort> client, PacketType packetType, TBody body)
        {
            var packet = new Packet<ushort, uint, ushort, TBody>()
            {
                TypeId = (ushort)packetType,
                Body = body
            };
            var (status, _) = await client.SendAsync(packet);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
        }
        
        public static async Task SendPacketAsync(this IStealthSharpClient<ushort, uint, ushort> client, PacketType packetType)
        {
            var packet = new Packet<ushort, uint, ushort>()
            {
                TypeId = (ushort)packetType
            };
            var (status, _) = await client.SendAsync(packet);
            if (!status)
                throw new InvalidOperationException("Fail to send packet");
        }
    }
}