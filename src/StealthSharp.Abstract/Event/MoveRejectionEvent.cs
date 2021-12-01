#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="MoveRejectionEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using StealthSharp.Enumeration;
using StealthSharp.Model;

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class MoveRejectionEvent
    {
        public WorldPoint Source { get; init; } = new();
        public Direction Direction { get; init;} = Direction.Unknown;
        public WorldPoint Destination { get; init;}= new();

        protected bool Equals(MoveRejectionEvent other)
        {
            return Source.Equals(other.Source) && Direction == other.Direction && Destination.Equals(other.Destination);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MoveRejectionEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Source, (int)Direction, Destination);
        }
    }
}