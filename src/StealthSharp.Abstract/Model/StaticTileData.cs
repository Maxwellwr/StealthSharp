#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StaticTileData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class StaticTileData
    {
        [PacketData(0, 8)] public ulong Flags { get; set; }

        [PacketData(8, 4)] public int Weight { get; set; }

        [PacketData(12, 4)] public int Height { get; set; }

        [PacketData(16, 1)] public byte RadarColorR { get; set; }

        [PacketData(17, 1)] public byte RadarColorG { get; set; }

        [PacketData(18, 1)] public byte RadarColorB { get; set; }

        [PacketData(19, PacketDataType = PacketDataType.Body)]
        public string Name { get; set; }
    }
}