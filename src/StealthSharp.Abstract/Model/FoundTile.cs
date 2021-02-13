#region Copyright

// -----------------------------------------------------------------------
// <copyright file="FoundTile.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace StealthSharp.Model
{
    /// <summary>
    ///     Found Tile.
    /// </summary>
    public class FoundTile
    {
        public ushort Tile { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
        public sbyte Z { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is FoundTile ft)
            {
                return ft.X == X && ft.Y == Y && ft.Z == Z;
            }

            // ReSharper disable once BaseObjectEqualsIsObjectEquals
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
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
            return obj.X.GetHashCode() ^ obj.Y.GetHashCode() ^ obj.Z.GetHashCode();
        }
    }
}