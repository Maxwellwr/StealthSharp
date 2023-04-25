#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MyPoint.cs" company="StealthSharp">
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
    ///     My Point.
    /// </summary>
    [Serialization.Serializable()]
    public record WorldPoint3D(ushort X, ushort Y, sbyte Z) : WorldPoint(X, Y)
    {
        public WorldPoint3D() : this(0, 0, 0)
        {
        }

        public void Deconstruct(out ushort x, out ushort y, out sbyte z)
        {
            (x, y) = this;
            z = Z;
        }


        public virtual bool Equals(WorldPoint3D? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Z == other.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Z);
        }
    }
}