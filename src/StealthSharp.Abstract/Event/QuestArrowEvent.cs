#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="QuestArrowEvent.cs" company="StealthSharp">
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
    public class QuestArrowEvent
    {
        public WorldPoint Point { get; init; } = new();
        public bool IsActive { get; init; }

        protected bool Equals(QuestArrowEvent other)
        {
            return Point.Equals(other.Point) && IsActive == other.IsActive;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((QuestArrowEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Point, IsActive);
        }
    }
}