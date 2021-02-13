#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Packet.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Enum;
using StealthSharp.Serialization;

namespace StealthSharp.Network
{
    [Serializable()]
    public class PacketHeader
    {
        public uint Length { get; set; }

        public PacketType PacketType { get; set; }

        public ushort CorrelationId { get; set; }
    }
}