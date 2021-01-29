#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LandTileData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     Land Tile Data.
    /// </summary>
    public class LandTileData
    {
        [PacketData(0, 4)] public uint Flags { get; set; }
        [PacketData(4, 4)] public uint Flags2 { get; set; }
        [PacketData(8, 2)] public ushort TextureId { get; set; }

        [PacketData(10, PacketDataType = PacketDataType.Body)]
        public string Name { get; set; }
    }
}