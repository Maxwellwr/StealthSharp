#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="UpdateObjectStatsEvent.cs" company="StealthSharp">
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
    public class UpdateObjectStatsEvent
    {
        public Identity Object { get; init; } = new();
        public uint CurrentLife { get; init; }
        public uint MaxLife { get; init; }
        public uint CurrentMana { get; init; }
        public uint MaxMana { get; init; }
        public uint CurrentStamina { get; init; }
        public uint MaxStamina { get; init; }

        protected bool Equals(UpdateObjectStatsEvent other)
        {
            return Object.Equals(other.Object) &&
                   CurrentLife == other.CurrentLife &&
                   MaxLife == other.MaxLife &&
                   CurrentMana == other.CurrentMana &&
                   MaxMana == other.MaxMana &&
                   CurrentStamina == other.CurrentStamina &&
                   MaxStamina == other.MaxStamina;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UpdateObjectStatsEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Object, CurrentLife, MaxLife, CurrentMana, MaxMana, CurrentStamina, MaxStamina);
        }
    }
}