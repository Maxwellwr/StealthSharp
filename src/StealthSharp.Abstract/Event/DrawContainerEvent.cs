#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="DrawContainerEvent.cs" company="StealthSharp">
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
    public class DrawContainerEvent
    {
        public Identity Container { get; init; } = new();
        public ushort ModelGump { get; init; }

        protected bool Equals(DrawContainerEvent other)
        {
            return Container.Equals(other.Container) && ModelGump == other.ModelGump;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DrawContainerEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Container, ModelGump);
        }
    }
}