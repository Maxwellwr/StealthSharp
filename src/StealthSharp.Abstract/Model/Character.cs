#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="Character.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class Character:Identity
    {
        public string Name { get; init; } = string.Empty;

        protected bool Equals(Character other)
        {
            return base.Equals(other) && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Character)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name);
        }
    }
}