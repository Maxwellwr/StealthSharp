#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="SoundEvent.cs" company="StealthSharp">
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
    public class SoundEvent
    {
        public Identity Sound { get; init; } = new();
        public WorldPoint3D Point { get; init; } = new();

        protected bool Equals(SoundEvent other)
        {
            return Sound.Equals(other.Sound) && Point.Equals(other.Point);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SoundEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sound, Point);
        }
    }
}