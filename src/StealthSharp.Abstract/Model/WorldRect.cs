#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="WorldRect.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model;

[Serialization.Serializable()]
public record WorldRect
{
    public required WorldPoint BottomLeft { get; init; }
    public required WorldPoint TopRight { get; init; }

    public ushort XMin => BottomLeft.X;
    public ushort YMin => BottomLeft.Y;
    public ushort XMax => TopRight.X;
    public ushort YMax => TopRight.Y;

    public ushort Width => (ushort)(XMax - XMin);
    public ushort Height => (ushort)(YMax - YMin);
    
    public WorldRect(ushort xmin, ushort ymin, ushort xmax, ushort ymax)
    {
        BottomLeft = new WorldPoint(xmin, ymin);
        TopRight = new WorldPoint(xmax, ymax);
    }

    public WorldRect(WorldPoint bottomLeft, WorldPoint topRight)
    {
        BottomLeft = bottomLeft;
        TopRight = topRight;
    }

    public WorldRect(WorldPoint center, ushort width, ushort depth)
    {
        var v = new WorldVector(checked((short)(width/2)), checked((short)(depth/2)));
        BottomLeft = center - v;
        TopRight = center + v;
    }

    public void Deconstruct(out WorldPoint bottomLeft, out WorldPoint topRight)
    {
        bottomLeft = BottomLeft;
        topRight = TopRight;
    }

    public void Deconstruct(out ushort xmin, out ushort ymin, out ushort xmax, out ushort ymax)
    {
        xmin = XMin;
        ymin = YMin;
        xmax = XMax;
        ymax = YMax;
    }

    public virtual bool Equals(WorldRect? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(BottomLeft, other.BottomLeft) && Equals(TopRight, other.TopRight);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BottomLeft.GetHashCode(), TopRight.GetHashCode());
    }
}