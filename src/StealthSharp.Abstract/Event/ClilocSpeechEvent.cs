#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="ClilocSpeechEvent.cs" company="StealthSharp">
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
    public class ClilocSpeechEvent
    {
        public Character Character { get; init; } = new();
        public Identity Cliloc { get; init; } = new();
        public string Text { get; init; } = string.Empty;

        protected bool Equals(ClilocSpeechEvent other)
        {
            return Character.Equals(other.Character) && Cliloc.Equals(other.Cliloc) && Text == other.Text;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ClilocSpeechEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Character, Cliloc, Text);
        }
    }
}