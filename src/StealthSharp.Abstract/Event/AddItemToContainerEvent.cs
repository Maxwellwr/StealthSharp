#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="AddItemToContainerEvent.cs" company="StealthSharp">
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
    public class AddItemToContainerEvent
    {
        public Identity Item { get; init; } = new();
        public Identity Container { get; init; } = new();

        protected bool Equals(AddItemToContainerEvent other)
        {
            return Item.Equals(other.Item) && Container.Equals(other.Container);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AddItemToContainerEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item, Container);
        }
    }
}