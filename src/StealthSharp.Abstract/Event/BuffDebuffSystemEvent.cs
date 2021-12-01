#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="BuffDebuffSystemEvent.cs" company="StealthSharp">
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
    public class BuffDebuffSystemEvent
    {
        public Identity Object { get; init; } = new();
        public ushort Attribute { get; init; }
        public bool IsEnabled { get; init; }

        protected bool Equals(BuffDebuffSystemEvent other)
        {
            return Object.Equals(other.Object) && Attribute == other.Attribute && IsEnabled == other.IsEnabled;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BuffDebuffSystemEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Object, Attribute, IsEnabled);
        }
    }
}