#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="SetGlobalVarEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class SetGlobalVarEvent
    {
        public string Name { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;

        protected bool Equals(SetGlobalVarEvent other)
        {
            return Name == other.Name && Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SetGlobalVarEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value);
        }
    }
}