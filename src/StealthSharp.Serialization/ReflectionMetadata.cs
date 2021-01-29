#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ReflectionMetadata.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class ReflectionMetadata : IReflectionMetadata
    {
        private readonly Dictionary<PacketDataType, PacketProperty> _packetProperties;
        public IReadOnlyList<PacketProperty> Properties { get; }

        public int IdLength =>
            Properties
                .Where(p => p.Attribute.PacketDataType == PacketDataType.Id)
                .Sum(p => p.Attribute.Length);

        public int MetaLength =>
            Properties
                .Where(p => p.Attribute.PacketDataType == PacketDataType.MetaData)
                .Sum(p => p.Attribute.Length);

        public int LengthLength =>
            Properties
                .Where(p => p.Attribute.PacketDataType == PacketDataType.Length)
                .Sum(p => p.Attribute.Length);

        public int TypeMapperLength =>
            Properties
                .Where(p => p.Attribute.PacketDataType == PacketDataType.TypeMapper)
                .Sum(p => p.Attribute.Length);

        public PacketProperty this[PacketDataType index] => _packetProperties[index];

        public bool Contains(PacketDataType packetDataType) => _packetProperties.ContainsKey(packetDataType);

        public IReflectionMetadata? BodyMetadata { get; }

        internal ReflectionMetadata(Type type, IReflectionLevelCache reflectionCache, bool firstLevel)
        {
            //EnsureTypeHasRequiredAttributes(type, firstLevel);
            Properties = GetTypeProperties(type);

            _packetProperties = Properties
                .Where(p => p.Attribute.PacketDataType != PacketDataType.MetaData)
                .ToDictionary(p => p.Attribute.PacketDataType);

            BodyMetadata =
                Contains(PacketDataType.Body)
                    ? reflectionCache.GetMetadata(this[PacketDataType.Body].PropertyType, false)
                    : null;
        }

        private static void EnsureTypeHasRequiredAttributes(Type type, bool firstLevel)
        {
            var properties = type.GetProperties();

            var key = properties.Where(item => GetTcpDataAttribute(item)!.PacketDataType == PacketDataType.Id).ToList();

            if (key.Count > 1)
                throw SerializationException.AttributeDuplicate(type.ToString(), nameof(PacketDataType.Id));

            if (key.Count == 1 && !CanReadWrite(key.Single()))
                throw SerializationException.PropertyCanReadWrite(type.ToString(), nameof(PacketDataType.Id));

            var body = properties
                .Where(item => GetTcpDataAttribute(item)!.PacketDataType == PacketDataType.Body)
                .ToList();

            var dynamic = properties
                .Where(item => GetTcpDataAttribute(item)!.PacketDataType == PacketDataType.Dynamic)
                .ToList();

            if (body.Count > 1)
                throw SerializationException.AttributeDuplicate(type.ToString(), nameof(PacketDataType.Body));

            if (dynamic.Count > 1)
                throw SerializationException.AttributeDuplicate(type.ToString(), nameof(PacketDataType.Dynamic));

            if (body.Count == 1 && !CanReadWrite(body.Single()))
                throw SerializationException.PropertyCanReadWrite(type.ToString(), nameof(PacketDataType.Body));

            if (dynamic.Count == 1 && !CanReadWrite(dynamic.Single()))
                throw SerializationException.PropertyCanReadWrite(type.ToString(), nameof(PacketDataType.Dynamic));

            if (body.Count == 1 && dynamic.Count == 1)
                throw SerializationException.AttributeInBody(type.ToString(), nameof(PacketDataType.Dynamic));

            if (dynamic.Count == 1 && dynamic.Single().PropertyType.GetProperties()
                .Any(p => p.PropertyType.GetInterfaces().All(i => i != typeof(IList))))
                throw SerializationException.AttributeInBody(type.ToString(), nameof(PacketDataType.Dynamic));

            var length = properties.Where(item => GetTcpDataAttribute(item)!.PacketDataType == PacketDataType.Length)
                .ToList();

            if (length.Count > 1)
                throw SerializationException.AttributeDuplicate(type.ToString(), nameof(PacketDataType.Length));

            if (firstLevel)
            {
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (length.Count == 0)
                    throw SerializationException.AttributeLengthRequired(type.ToString(), nameof(PacketDataType.Body));

                if (length.Count == 1 && !CanReadWrite(length.Single()))
                    throw SerializationException.PropertyCanReadWrite(type.ToString(), nameof(PacketDataType.Length));
            }
            else
            {
                if (length.Count == 1)
                    throw SerializationException.AttributeInBody(type.ToString(), nameof(PacketDataType.Length));
            }

            var metaData = properties.Where(item => GetTcpDataAttribute(item).PacketDataType == PacketDataType.MetaData)
                .ToList();

            if (firstLevel)
            {
                if (key.Count == 0 && length.Count == 0 && body.Count == 0 && metaData.Count == 0)
                    throw SerializationException.AttributesRequired(type.ToString());
            }
            else
            {
                if (body.Count == 0 && metaData.Count == 0)
                    throw SerializationException.AttributesRequired(type.ToString());
            }

            foreach (var item in metaData.Where(item => !CanReadWrite(item)))
                throw SerializationException.PropertyCanReadWrite(type.ToString(),
                    nameof(PacketDataType.MetaData),
                    GetTcpDataAttribute(item)!.Index.ToString());

            static bool CanReadWrite(PropertyInfo property) => property.CanRead && property.CanWrite;
        }

        private static List<PacketProperty> GetTypeProperties(Type type)
        {
            var packetProperties = new List<PacketProperty>();

            foreach (var property in type.GetProperties())
            {
                var attribute = GetTcpDataAttribute(property);

                if (attribute == null)
                    continue;

                PacketProperty packetProperty = new(property, attribute);

                packetProperties.Add(packetProperty);
            }

            return packetProperties;
        }

        private static PacketDataAttribute? GetTcpDataAttribute(ICustomAttributeProvider property)
            => property
                .GetCustomAttributes(true)
                .OfType<PacketDataAttribute>()
                .SingleOrDefault();
    }
}