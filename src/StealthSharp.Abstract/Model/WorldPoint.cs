#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Point.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     Point.
    /// </summary>
    [Serialization.Serializable()]
    public class WorldPoint
    {
        public WorldPoint()
        {
            X = 0;
            Y = 0;
        }
        
        public WorldPoint(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out ushort x, out ushort y)
        {
            x = X;
            y = Y;
        }

        public ushort X { get; init; }
        public ushort Y { get; init; }

        protected bool Equals(WorldPoint other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WorldPoint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}