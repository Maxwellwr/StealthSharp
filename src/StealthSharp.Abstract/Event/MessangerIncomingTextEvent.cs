#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="MessangerIncomingTextEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using StealthSharp.Enumeration;

#endregion

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class MessengerIncomingTextEvent
    {
        public MessengerType MessengerType { get; init; }
        public string SenderNickname { get; init; } = "";
        public string SenderId { get; init; } = "";
        public string ChatId { get; init; } = "";
        public string EventMsg { get; init; } = "";
        public MessengerEventType EventType { get; init; }

        protected bool Equals(MessengerIncomingTextEvent other)
        {
            return MessengerType == other.MessengerType &&
                   SenderNickname == other.SenderNickname &&
                   SenderId == other.SenderId &&
                   ChatId == other.ChatId &&
                   EventMsg == other.EventMsg &&
                   EventType == other.EventType;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MessengerIncomingTextEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)MessengerType, SenderNickname, SenderId, ChatId, EventMsg, (int)EventType);
        }
    }
}