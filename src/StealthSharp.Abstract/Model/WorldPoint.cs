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

namespace StealthSharp.Model;

/// <summary>
///     Point.
/// </summary>
[Serialization.Serializable()]
public record WorldPoint(ushort X, ushort Y)
{
    public WorldPoint() : this(0, 0)
    {
    }

    public void Deconstruct(out ushort x, out ushort y)
    {
        x = X;
        y = Y;
    }

    public virtual bool Equals(WorldPoint? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
        
    public static WorldPoint operator +(WorldPoint point, WorldVector vector) => 
        new(checked((ushort)(point.X + vector.X)), checked((ushort)(point.Y + vector.Y)));

    public static WorldPoint operator -(WorldPoint point, WorldVector vector) => 
        new(checked((ushort)(point.X - vector.X)), checked((ushort)(point.Y - vector.Y)));
}