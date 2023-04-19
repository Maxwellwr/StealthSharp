#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="CharAnimationEvent.cs" company="StealthSharp">
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
    public class CharAnimationEvent
    {
        public Identity Object { get; init; } = new();
        public ushort Action { get; init; }

        protected bool Equals(CharAnimationEvent other)
        {
            return Object.Equals(other.Object) && Action == other.Action;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CharAnimationEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Object, Action);
        }
    }
}