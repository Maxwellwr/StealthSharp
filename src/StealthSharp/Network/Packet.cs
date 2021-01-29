#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Packet.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Network
{
    public class Packet<TId, TSize, TMapping> : IPacket<TId, TSize, TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        [PacketData(0, 4, PacketDataType = PacketDataType.Length)]
        public TSize Length { get; set; }

        [PacketData(4, 2, PacketDataType = PacketDataType.TypeMapper)]
        public TMapping TypeId { get; set; }

        [PacketData(6, 2, PacketDataType = PacketDataType.Id)]
        public TId CorrelationId { get; set; }
    }

    public class Packet<TId, TSize, TMapping, T> : Packet<TId, TSize, TMapping>, IPacket<TId, TSize, TMapping, T>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        [PacketData(8, PacketDataType = PacketDataType.Body)]
        public T? Body { get; set; }
    }
}