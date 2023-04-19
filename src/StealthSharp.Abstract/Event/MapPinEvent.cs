#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="MapPinEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class MapPinEvent
    {
        public Identity Identity { get; init; } = new();
        public byte Action { get; init; }
        public byte PinId { get; init; }
        public WorldPoint Point { get; init; } = new();

        protected bool Equals(MapPinEvent other)
        {
            return Identity.Equals(other.Identity) &&
                   Action == other.Action &&
                   PinId == other.PinId &&
                   Point.Equals(other.Point);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MapPinEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identity, Action, PinId, Point);
        }
    }
}