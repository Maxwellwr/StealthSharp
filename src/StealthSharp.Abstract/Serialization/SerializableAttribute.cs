#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="PacketAttribute.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SerializableAttribute : Attribute
    {
        public Endianness Endianness { get; }

        public SerializableAttribute(Endianness endianness = Endianness.LittleEndian)
        {
            Endianness = endianness;
        }
    }
}