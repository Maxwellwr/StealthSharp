#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MyPoint.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model
{
    /// <summary>
    ///     My Point.
    /// </summary>
    [Serialization.Serializable()]
    public class WorldPoint3D: WorldPoint
    {
        public sbyte Z { get; init; }

        protected bool Equals(WorldPoint3D other)
        {
            return base.Equals(other) && Z == other.Z;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WorldPoint3D)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Z);
        }
    }
}