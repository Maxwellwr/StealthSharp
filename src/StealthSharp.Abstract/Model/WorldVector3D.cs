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
public record WorldVector3D(short X,short Y,byte Z): WorldVector(X,Y)
{
    public WorldVector3D(): this(0,0,0)
    {
    }
    
    public virtual bool Equals(WorldVector3D? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Z);
    }

    public static WorldVector3D operator *(WorldVector3D vector, float k) =>
        new(checked((short)(vector.X * k)), checked((short)(vector.Y * k)), checked((byte)(vector.Z * k)));

    public static WorldVector3D operator /(WorldVector3D vector, float k) => vector * (1 / k);

    public static WorldVector3D operator +(WorldVector3D a, WorldVector3D b) => 
        new(checked((short)(a.X + b.X)), checked((short)(a.Y + b.Y)), checked((byte)(a.Z + b.Z)));

    public static WorldVector3D operator -(WorldVector3D a, WorldVector3D b) => 
        new(checked((short)(a.X - b.X)), checked((short)(a.Y - b.Y)), checked((byte)(a.Z - b.Z)));
}