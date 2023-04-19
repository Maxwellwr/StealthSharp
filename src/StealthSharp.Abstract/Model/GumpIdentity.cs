#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="GumpIdentity.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class GumpIdentity
    {
        public uint Serial { get; init; }

        public uint Id { get; init; }

        protected bool Equals(GumpIdentity other)
        {
            return Serial == other.Serial && Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GumpIdentity)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Serial, Id);
        }
    }
}