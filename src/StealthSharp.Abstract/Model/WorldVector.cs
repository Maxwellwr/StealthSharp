#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="WorldVector.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model;

[Serialization.Serializable()]
public record WorldVector(short X, short Y)
{
    public WorldVector() : this(0, 0)
    {
    }

    public virtual bool Equals(WorldVector? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static WorldVector operator *(WorldVector vector, float k) => new(checked((short)(vector.X * k)), checked((short)(vector.Y * k)));

    public static WorldVector operator /(WorldVector vector, float k) => vector * (1 / k);

    public static WorldVector operator +(WorldVector a, WorldVector b) => new(checked((short)(a.X + b.X)), checked((short)(a.Y + b.Y)));

    public static WorldVector operator -(WorldVector a, WorldVector b) => new(checked((short)(a.X - b.X)), checked((short)(a.Y - b.Y)));
}