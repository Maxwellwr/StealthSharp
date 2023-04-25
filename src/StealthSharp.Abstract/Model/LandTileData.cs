#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LandTileData.cs" company="StealthSharp">
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
    /// <summary>
    ///     Land Tile Data.
    /// </summary>
    [Serializable()]
    public class LandTileData
    {
        public TileDataFlags Flags { get; set; }
        public ushort TextureId { get; set; }


        public string Name { get; set; } = string.Empty;
    }
}