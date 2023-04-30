#region Copyright

// -----------------------------------------------------------------------
// <copyright file="FoundTile.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using StealthSharp.Serialization;

#endregion

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace StealthSharp.Model
{
    /// <summary>
    ///     Found Tile.
    /// </summary>
    [Serializable()]
    public class FoundTile
    {
        public ushort Tile { get; set; }
        public WorldPoint3D Point { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is FoundTile ft) return ft.Point == Point && ft.Tile == Tile;

            // ReSharper disable once BaseObjectEqualsIsObjectEquals
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return Point.GetHashCode()^Tile.GetHashCode();
        }
    }

    public class FoundTileComparer : IEqualityComparer<FoundTile>
    {
        public bool Equals(FoundTile? x, FoundTile? y)
        {
            if (x is null && y is null)
                return true;
            if (x is null || y is null)
                return false;
            return x.Equals(y);
        }

        public int GetHashCode(FoundTile obj)
        {
            return obj.Point.GetHashCode() ^ obj.Tile;
        }
    }
}