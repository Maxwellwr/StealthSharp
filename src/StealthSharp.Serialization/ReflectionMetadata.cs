#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ReflectionMetadata.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class ReflectionMetadata : IReflectionMetadata
    {
        public IReadOnlyList<PacketProperty> Properties { get; }
        public Endianness Endianness { get; }

        internal ReflectionMetadata(Type type)
        {
            Endianness = type.GetCustomAttribute<SerializableAttribute>()?.Endianness ?? Endianness.LittleEndian;
            Properties = GetTypeProperties(type);
        }
        private static List<PacketProperty> GetTypeProperties(Type type)
        {
            var packetProperties = new List<PacketProperty>();

            foreach (var property in type.GetProperties().OrderBy(p => p.MetadataToken))
            {
                PacketProperty packetProperty = new(property);
                packetProperties.Add(packetProperty);
            }

            return packetProperties;
        }

    }
}