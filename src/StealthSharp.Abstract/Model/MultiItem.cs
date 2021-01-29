#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MultiItem.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class MultiItem
    {
        [PacketData(0, 4)] public uint ID { get; set; }
        [PacketData(4, 2)] public ushort X { get; set; }
        [PacketData(6, 2)] public ushort Y { get; set; }
        [PacketData(8, 1)] public sbyte Z { get; set; }
        [PacketData(9, 2)] public ushort XMin { get; set; }
        [PacketData(11, 2)] public ushort XMax { get; set; }
        [PacketData(13, 2)] public ushort YMin { get; set; }
        [PacketData(15, 2)] public ushort YMax { get; set; }
        [PacketData(17, 2)] public ushort Width { get; set; }
        [PacketData(19, 2)] public ushort Height { get; set; }
    }
}