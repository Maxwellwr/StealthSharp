#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="AllowRefuseAttackEvent.cs" company="StealthSharp">
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
    public class AllowRefuseAttackEvent
    {
        public Identity Target { get; init; } = new();
        public bool IsAttackOk { get; init; }

        protected bool Equals(AllowRefuseAttackEvent other)
        {
            return Target.Equals(other.Target) && IsAttackOk == other.IsAttackOk;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AllowRefuseAttackEvent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Target, IsAttackOk);
        }
    }
}