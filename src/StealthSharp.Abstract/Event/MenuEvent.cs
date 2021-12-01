#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="DialogEvent.cs" company="StealthSharp">
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
    public class MenuEvent
    {
        public Identity Dialog { get; init; } = new ();
        public Identity Menu { get; init; } = new();

        protected bool Equals(MenuEvent other)
        {
            return Dialog.Equals(other.Dialog) && Menu.Equals(other.Menu);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MenuEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Dialog, Menu);
        }
    }
}