#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="IncomingGumpEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using StealthSharp.Model;

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class IncomingGumpEvent
    {
        public GumpIdentity Gump { get; init; } = new();
        public ScreenPoint Point { get; init; } = new();

        protected bool Equals(IncomingGumpEvent other)
        {
            return Gump.Equals(other.Gump) && Point.Equals(other.Point);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IncomingGumpEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Gump, Point);
        }
    }
}