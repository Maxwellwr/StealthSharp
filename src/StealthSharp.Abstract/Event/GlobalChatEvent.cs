#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="GlobalChatEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class GlobalChatEvent
    {
        public ushort MsgCode { get; init; }
        public string Name { get; init; } = "";
        public string Text { get; init; } = "";

        protected bool Equals(GlobalChatEvent other)
        {
            return MsgCode == other.MsgCode && Name == other.Name && Text == other.Text;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GlobalChatEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MsgCode, Name, Text);
        }
    }
}