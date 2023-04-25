#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StaticTileData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Enumeration;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class StaticTileData
    {
        public TileDataFlags Flags { get; set; }
        public ushort Weight { get; set; }
        public ushort AnimId { get; set; }
        public int Height { get; set; }
        public byte RadarColorR { get; set; }
        public byte RadarColorG { get; set; }
        public byte RadarColorB { get; set; }
        public byte RadarColorA { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}