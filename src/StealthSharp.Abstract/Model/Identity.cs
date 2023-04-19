#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="BaseObject.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class Identity
    {
        public uint Id { get; init; }

        public bool Equals(Identity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Identity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }
    }
}