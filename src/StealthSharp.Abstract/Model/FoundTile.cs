#region Copyright

// -----------------------------------------------------------------------
// <copyright file="FoundTile.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     Found Tile.
    /// </summary>
    public class FoundTile
    {
        [PacketData(0, 2)] public ushort Tile { get; set; }
        [PacketData(2, 2)] public ushort X { get; set; }
        [PacketData(4, 2)] public ushort Y { get; set; }
        [PacketData(6, 1)] public sbyte Z { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is FoundTile ft)
            {
                return ft.X == X && ft.Y == Y && ft.Z == Z;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
    }

    public class FoundTileComparer : IEqualityComparer<FoundTile>
    {
        public bool Equals(FoundTile x, FoundTile y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(FoundTile obj)
        {
            return obj.X.GetHashCode() ^ obj.Y.GetHashCode() ^ obj.Z.GetHashCode();
        }
    }
}