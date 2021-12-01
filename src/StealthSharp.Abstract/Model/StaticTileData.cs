#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StaticTileData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class StaticTileData
    {
        public ulong Flags { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public byte RadarColorR { get; set; }
        public byte RadarColorG { get; set; }
        public byte RadarColorB { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}