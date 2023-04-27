#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MapCell.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     Map Cell.
    /// </summary>
    [Serializable]
    public record MapCell
    {
        public MapCell()
        {
        }
        /// <summary>
        ///     Map Cell.
        /// </summary>
        public MapCell(ushort tile, sbyte z)
        {
            Tile = tile;
            Z = z;
        }

        public ushort Tile { get; init; }
        public sbyte Z { get; init; }

        public void Deconstruct(out ushort tile, out sbyte z)
        {
            tile = Tile;
            z = Z;
        }
    }
}