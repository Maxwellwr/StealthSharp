#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="GumpTextEntryEvent.cs" company="StealthSharp">
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
    public class GumpTextEntryEvent
    {
        public Identity Identity { get; init; } = new ();
        public string Title { get; init; } = "";
        public byte InputStyle { get; init; }
        public uint MaxValue { get; init; }
        public string Title2 { get; init; } = "";

        protected bool Equals(GumpTextEntryEvent other)
        {
            return Identity.Equals(other.Identity) && 
                   Title == other.Title && 
                   InputStyle == other.InputStyle &&
                   MaxValue == other.MaxValue && 
                   Title2 == other.Title2;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GumpTextEntryEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identity, Title, InputStyle, MaxValue, Title2);
        }
    }
}