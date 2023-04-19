#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="ServerEventDataConverter.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Linq;
using StealthSharp.Enumeration;
using StealthSharp.Event;

#endregion

namespace StealthSharp.Serialization.Converters
{
    public class ServerEventDataConverter : ICustomConverter<ServerEventData>
    {
        private readonly IMarshaler _marshaler;
        private readonly IReflectionCache _reflectionCache;

        public ServerEventDataConverter(IMarshaler marshaler, IReflectionCache reflectionCache)
        {
            _marshaler = marshaler;
            _reflectionCache = reflectionCache;
        }

        public bool TryConvertToBytes(object? propertyValue, in Span<byte> span,
            Endianness endianness = Endianness.LittleEndian)
        {
            if (propertyValue is not ServerEventData se)
                return false;

            try
            {
                var eventDataType = se.EventType.GetEnumDataType();
                if (eventDataType is null)
                    return false;


                var propertyType = typeof(ServerEventData<>).MakeGenericType(eventDataType);
                var data = propertyType.GetProperty(nameof(ServerEventData<object>.EventData))?.GetValue(propertyValue);
                if (data is null)
                    return false;

                var position = _marshaler.SizeOf(se.EventType);
                _marshaler.Serialize(span, se.EventType, endianness);

                var metadata = _reflectionCache.GetMetadata(eventDataType);

                _marshaler.Serialize(span.Slice(position++, 1), GetTotalParametersCount(metadata));
                return Serialize(metadata, span[position..], data, endianness) == span.Length - position;
            }
            catch
            {
                return false;
            }
        }

        private int Serialize(IReflectionMetadata? metadata, in Span<byte> span, object data, Endianness endianness)
        {
            var position = 0;
            if (metadata is null)
            {
                _marshaler.Serialize(span[position++..], GetParameterType(data.GetType()),
                    endianness);
                _marshaler.Serialize(span[position..], data, endianness);
                position += _marshaler.SizeOf(data);
            }
            else
            {
                foreach (var property in metadata.Properties)
                {
                    var eventPropertyValue = property.Get(data);
                    if (eventPropertyValue is null) return position;

                    var propertyMetadata = _reflectionCache.GetMetadata(property.PropertyType);
                    position += Serialize(propertyMetadata, span[position..], eventPropertyValue, endianness);
                }
            }

            return position;
        }

        public bool TryConvertFromBytes(out object? propertyValue, in Span<byte> span,
            Endianness endianness = Endianness.LittleEndian)
        {
            propertyValue = null;
            try
            {
                _marshaler.Deserialize(span, typeof(EventType), out var value, endianness);

                var eventType = (EventType)value;
                var eventDataType = eventType.GetEnumDataType();
                if (eventDataType is null)
                    return false;
                var size = _marshaler.SizeOf(value);

                var propertyType = typeof(ServerEventData<>).MakeGenericType(eventDataType);

                var eventDataValue = ActivatorHelper.CreateInstanceParameterless(eventDataType);
                if (eventDataValue is null)
                    throw StealthSharpException.ErrorCreateType(eventDataType);

                var metadata = _reflectionCache.GetMetadata(eventDataType);
                _marshaler.Deserialize(span[size++..], typeof(byte), out var paramCount, endianness);
                if (GetTotalParametersCount(metadata) != (byte)paramCount)
                    return false;

                size += Deserialize(metadata, span[size..], ref eventDataValue, endianness);

                propertyValue = ActivatorHelper.CreateInstance(propertyType, eventType, eventDataValue);


                return size == span.Length;
            }
            catch
            {
                return false;
            }
        }

        private int Deserialize(IReflectionMetadata? metadata, Span<byte> span, ref object eventDataValue,
            Endianness endianness)
        {
            var position = 0;
            if (metadata is null)
            {
                _marshaler.Deserialize(span[position++..], typeof(ParameterType), out var paramType, endianness);

                _marshaler.Deserialize(span[position..], GetTypeForParameter((ParameterType)paramType)
                    , out var paramValue, endianness);
                position += _marshaler.SizeOf(paramValue);
                var valueType = eventDataValue.GetType();
                eventDataValue = valueType.IsEnum ? Enum.ToObject(valueType, paramValue) : Convert.ChangeType(paramValue, valueType);
            }
            else
            {
                foreach (var property in metadata.Properties)
                {
                    var propertyMetadata = _reflectionCache.GetMetadata(property.PropertyType);
                    var eventPropertyValue = ActivatorHelper.CreateInstanceParameterless(property.PropertyType);

                    position += Deserialize(propertyMetadata, span[position..], ref eventPropertyValue, endianness);

                    property.Set(eventDataValue, eventPropertyValue);
                }
            }

            return position;
        }


        public int SizeOf(object? propertyValue)
        {
            if (propertyValue is not ServerEventData se) return 0;

            var eventDataType = se.EventType.GetEnumDataType();
            if (eventDataType is null)
                return 0;
            var metadata = _reflectionCache.GetMetadata(eventDataType);
            var eventData = propertyValue.GetType().GetProperty(nameof(ServerEventData<object>.EventData))
                ?.GetValue(propertyValue);
            return
                _marshaler.SizeOf(se.EventType)
                + 1 //param count
                + SizeOf(metadata, eventData);
        }

        private int SizeOf(IReflectionMetadata? metadata, object? data)
        {
            if (data is null)
                return 0;
            if (metadata is null)
                return _marshaler.SizeOf(data) + _marshaler.SizeOf(typeof(ParameterType));

            return metadata.Properties.Aggregate(0,
                (current, property) =>
                    current
                    + SizeOf(_reflectionCache.GetMetadata(property.PropertyType), property.Get(data)));
        }

        private byte GetTotalParametersCount(IReflectionMetadata? metadata)
        {
            return metadata is null
                ? (byte)1
                : metadata.Properties.Aggregate<PacketProperty, byte>(0,
                    (current, property) =>
                        (byte)(current + GetTotalParametersCount(_reflectionCache.GetMetadata(property.PropertyType))));
        }

        private static ParameterType GetParameterType(Type propertyType)
        {
            return (propertyType.IsEnum ? propertyType.GetEnumUnderlyingType() : propertyType) switch
            {
                { } stringType when stringType == typeof(string) => ParameterType.String,
                { } uintType when uintType == typeof(uint) => ParameterType.Uint,
                { } intType when intType == typeof(int) => ParameterType.Int,
                { } ushortType when ushortType == typeof(ushort) => ParameterType.Ushort,
                { } shortType when shortType == typeof(short) => ParameterType.Short,
                { } byteType when byteType == typeof(byte) => ParameterType.Byte,
                { } sbyteType when sbyteType == typeof(sbyte) => ParameterType.Sbyte,
                { } boolType when boolType == typeof(bool) => ParameterType.Bool,
                _ => throw StealthSharpException.UnknownParameterType(propertyType)
            };
        }

        private static Type GetTypeForParameter(ParameterType paramType)
        {
            return paramType switch
            {
                ParameterType.String => typeof(string),
                ParameterType.Uint => typeof(uint),
                ParameterType.Int => typeof(int),
                ParameterType.Ushort => typeof(ushort),
                ParameterType.Short => typeof(short),
                ParameterType.Sbyte => typeof(sbyte),
                ParameterType.Byte => typeof(byte),
                ParameterType.Bool => typeof(bool),
                _ => throw StealthSharpException.UnknownParameterType(paramType)
            };
        }
    }
}