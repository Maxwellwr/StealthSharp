#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="GraphicalEffectEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using StealthSharp.Enumeration;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class GraphicalEffectEvent
    {
        public Identity Source { get; init; } = new();
        public WorldPoint3D SourcePoint { get; init; } = new();
        public Identity Destination { get; init; } = new();
        public WorldPoint3D DestinationPoint { get; init; } = new();
        public byte Type { get; init; }
        public ushort ItemId { get; init; }
        public Direction FixedDirection { get; init; }

        protected bool Equals(GraphicalEffectEvent other)
        {
            return Source.Equals(other.Source) &&
                   SourcePoint.Equals(other.SourcePoint) &&
                   Destination.Equals(other.Destination) &&
                   DestinationPoint.Equals(other.DestinationPoint) &&
                   Type == other.Type &&
                   ItemId == other.ItemId &&
                   FixedDirection == other.FixedDirection;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GraphicalEffectEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Source, SourcePoint, Destination, DestinationPoint, Type, ItemId,
                (int)FixedDirection);
        }
    }
}