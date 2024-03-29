﻿#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="MapMessageEvent.cs" company="StealthSharp">
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
    public class MapMessageEvent
    {
        public Identity Item { get; init; } = new();
        public WorldPoint WorldPoint { get; init; } = new();

        protected bool Equals(MapMessageEvent other)
        {
            return Item.Equals(other.Item) && WorldPoint.Equals(other.WorldPoint);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MapMessageEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item, WorldPoint);
        }
    }
}