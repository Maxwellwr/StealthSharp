#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="EmptyEvent.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

namespace StealthSharp.Event
{
    [Serialization.Serializable()]
    public class EmptyEvent
    {
        protected bool Equals(EmptyEvent other)
        {
            return true;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmptyEvent)obj);
        }

        public override int GetHashCode()
        {
            return 0.GetHashCode();
        }
    }
}