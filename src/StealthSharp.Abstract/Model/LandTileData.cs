#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LandTileData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     Land Tile Data.
    /// </summary>
    public class LandTileData
    {
        public uint Flags { get; set; }
        public uint Flags2 { get; set; }
        public ushort TextureId { get; set; }

        
        public string Name { get; set; }
    }
}