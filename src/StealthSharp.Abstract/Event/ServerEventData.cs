#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="ItemEventArgs.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using StealthSharp.Enumeration;

namespace StealthSharp.Event
{
    public class ServerEventData<T>: ServerEventData
    {
        public T EventData { get; init; }

        public override object GetEventData() => EventData!;

        public ServerEventData(EventType eventType, [DisallowNull] T eventData)
        {
            EventType = eventType;
            EventData = eventData ?? throw new ArgumentNullException(nameof(eventData));
        }
        
        protected bool Equals(ServerEventData<T> other)
        {
            return base.Equals(other) && EqualityComparer<T?>.Default.Equals(EventData, other.EventData);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ServerEventData<T>)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), EventData);
        }
    }
    
    public abstract class ServerEventData
    {
        public EventType EventType { get; init; }

        public abstract object GetEventData();
        
        protected bool Equals(ServerEventData other)
        {
            return EventType == other.EventType;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(ServerEventData) && Equals((ServerEventData) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EventType);
        }

        public static bool operator ==(ServerEventData left, ServerEventData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ServerEventData left, ServerEventData right)
        {
            return !Equals(left, right);
        }
    }
}