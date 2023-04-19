#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="SpeechEvent.cs" company="StealthSharp">
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
    public class SpeechEvent
    {
        public string Text { get; init; } = "";
        public string SenderName { get; init; } = "";
        public Identity Sender { get; init; } = new();

        public bool Equals(SpeechEvent other)
        {
            return Text == other.Text && SenderName == other.SenderName && Sender.Equals(other.Sender);
        }

        public override bool Equals(object? obj)
        {
            return obj is SpeechEvent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Text, SenderName, Sender);
        }
    }
}