#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ReflectionMetadata.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace StealthSharp.Serialization
{
    public class ReflectionMetadata : IReflectionMetadata
    {
        public IReadOnlyList<PacketProperty> Properties { get; }
        public Endianness Endianness { get; }

        internal ReflectionMetadata(Type type)
        {
            var attribute = type.GetCustomAttribute<SerializableAttribute>();
            Endianness = attribute?.Endianness ?? Endianness.LittleEndian;
            Properties = GetTypeProperties(type).ToArray();
        }

        private static IEnumerable<PacketProperty> GetTypeProperties(Type? type)
        {
            if (type is null)
                return Enumerable.Empty<PacketProperty>();
            return GetTypeProperties(type.BaseType).Concat(type
                .GetTypeInfo()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public| BindingFlags.DeclaredOnly)
                .OrderBy(p => p.MetadataToken)
                .Select(property => new PacketProperty(property)));
        }
    }
}