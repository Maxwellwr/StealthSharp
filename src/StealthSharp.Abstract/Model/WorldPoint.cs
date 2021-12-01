#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Point.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model
{
    /// <summary>
    ///     Point.
    /// </summary>
    [Serialization.Serializable()]
    public class WorldPoint
    {
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