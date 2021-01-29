#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketSerializer.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Options;

namespace StealthSharp.Serialization
{
    public class PacketSerializer : IPacketSerializer
    {
        private readonly SerializationOptions _options;
        private readonly IReflectionCache _reflectionCache;
        private readonly ICustomConverterFactory? _customConverterFactory;
        private readonly IBitConvert _bitConvert;

        public PacketSerializer(IOptions<SerializationOptions> options, 
            IReflectionCache reflectionCache, 
            ICustomConverterFactory? customConverterFactory, 
            IBitConvert bitConvert)
        {
            _options = options?.Value ?? new SerializationOptions();
            _reflectionCache = reflectionCache ?? throw new ArgumentNullException(nameof(reflectionCache));
            _customConverterFactory = customConverterFactory?? throw new ArgumentNullException(nameof(customConverterFactory));
            _bitConvert = bitConvert ?? throw new ArgumentNullException(nameof(bitConvert));
        }

        public ISerializationResult Serialize(object data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var reflectionMetadata = _reflectionCache.GetMetadata(data.GetType());
            var realLength = _bitConvert.SizeOf(data);

            if (reflectionMetadata != null && reflectionMetadata.Contains(PacketDataType.Length))
            {
                var lengthProperty = reflectionMetadata[PacketDataType.Length];
                var lengthValue = lengthProperty.PropertyType == typeof(int)
                    ? realLength
                    : Convert.ChangeType(realLength, lengthProperty.PropertyType);

                lengthProperty.Set(data, lengthValue);
            }

            var serializationResult = new SerializationResult(realLength + (reflectionMetadata?.LengthLength ?? 0));

            InnerSerialize(reflectionMetadata, serializationResult.Memory.Span, data);

            return serializationResult;
        }

        public void Serialize(in Span<byte> span, object data, Endianness endianness = Endianness.LittleEndian) => 
            ConvertToBytes(data, span, endianness);

        public object Deserialize(ISerializationResult data, Type targetType)
        {
            var reflectionMetadata = _reflectionCache.GetMetadata(targetType);

            InnerDeserialize(reflectionMetadata, data.Memory.Span, out var deserializationResult, targetType);

            return deserializationResult;
        }
        
        public void Deserialize(in Span<byte> span, Type dataType, out object value, Endianness endianness = Endianness.LittleEndian) => 
            ConvertFromBytes(out value, dataType, span, endianness);

        private void InnerSerialize(IReflectionMetadata? reflectionMetadata, in Span<byte> span, object data)
        {
            if (reflectionMetadata != null)
            {
                foreach (var property in reflectionMetadata.Properties)
                {
                    var propertyValue = property.Get(data);
                    if (propertyValue != null)
                    {
                        if (property.Attribute.PacketDataType == PacketDataType.Body)
                        {
                            if (reflectionMetadata.BodyMetadata == null)
                            {
                                ConvertToBytes(
                                    propertyValue,
                                    span.Slice(property.Attribute.Index),
                                    property.Attribute.Endianness);
                            }
                            else
                            {
                                InnerSerialize(reflectionMetadata.BodyMetadata, span.Slice(property.Attribute.Index),
                                    propertyValue);
                            }
                        }
                        else if (property.Attribute.PacketDataType == PacketDataType.Dynamic)
                        {
                            var index = property.Attribute.Index;
                            foreach (var propertyInfo in property.PropertyType.GetProperties())
                            {
                                var pv = propertyInfo.GetValue(propertyValue) ??
                                         Activator.CreateInstance(propertyInfo.PropertyType, 0);
                                if (pv is null)
                                    throw new InvalidOperationException();

                                ConvertToBytes(pv,
                                    span.Slice(index),
                                    property.Attribute.Endianness);
                                index += _bitConvert.SizeOf(pv);
                            }
                        }
                        else
                        {
                            ConvertToBytes(
                                propertyValue,
                                span.Slice(property.Attribute.Index, property.Attribute.Length),
                                property.Attribute.Endianness);
                        }
                    }
                }
            }
            else
            {
                ConvertToBytes(
                    data,
                    span);
            }
        }

         private void ConvertToBytes(
            object? propertyValue,
            in Span<byte> span,
            Endianness endianness = Endianness.LittleEndian)
        {
            switch (propertyValue)
            {
                case null:
                    throw SerializationException.PropertyArgumentIsNull(nameof(propertyValue));
                case byte @byte:
                    CheckSpanSize<byte>(span);
                    span[0] = @byte;
                    break;
                case sbyte @sbyte:
                    CheckSpanSize<sbyte>(span);
                    span[0] = (byte) @sbyte;
                    break;
                case bool @bool:
                    CheckSpanSize<bool>(span);
                    span[0] = (@bool ? (byte) 1 : (byte) 0);
                    break;
                case char @char:
                    CheckSpanSize<char>(span);
                    Unsafe.As<byte, char>(ref span[0]) = @char;
                    break;
                case double @double:
                    CheckSpanSize<double>(span);
                    Unsafe.As<byte, double>(ref span[0]) = @double;
                    break;
                case short @short:
                    CheckSpanSize<short>(span);
                    Unsafe.As<byte, short>(ref span[0]) = @short;
                    break;
                case ushort @ushort:
                    CheckSpanSize<ushort>(span);
                    Unsafe.As<byte, ushort>(ref span[0]) = @ushort;
                    break;
                case int @int:
                    CheckSpanSize<int>(span);
                    Unsafe.As<byte, int>(ref span[0]) = @int;
                    break;
                case uint @uint:
                    CheckSpanSize<uint>(span);
                    Unsafe.As<byte, uint>(ref span[0]) = @uint;
                    break;
                case long @long:
                    CheckSpanSize<long>(span);
                    Unsafe.As<byte, long>(ref span[0]) = @long;
                    break;
                case ulong @ulong:
                    CheckSpanSize<ulong>(span);
                    Unsafe.As<byte, ulong>(ref span[0]) = @ulong;
                    break;
                case float @float:
                    CheckSpanSize<float>(span);
                    Unsafe.As<byte, float>(ref span[0]) = @float;
                    break;
                case string @string:
                    if (span.Length < @string.Length * 2 + _bitConvert.SizeOf(typeof(int)))
                        // ReSharper disable once NotResolvedInText
                        throw new ArgumentOutOfRangeException("serializedArray",
                            $"Array length lower, then size of type {nameof(String)}");
                    ConvertToBytes(@string.Length * 2, span, endianness);
                    Encoding.Unicode.GetBytes(@string).CopyTo(span.Slice(4));
                    return;
                case System.Enum @enum:
                    ConvertToBytes(Convert.ChangeType(@enum, @enum.GetType().GetEnumUnderlyingType()), span,
                        endianness);
                    return;
                default:
                    if (propertyValue is IList collection)
                    {
                        var sizeType = _options.ArrayCountType;
                        if (span.Length < _bitConvert.SizeOf(propertyValue))
                            throw new ArgumentOutOfRangeException(nameof(span),
                                $"Array length lower, then size of {collection.GetType().GetElementType()} array ");

                        var index = 0;
                        ConvertToBytes(
                            sizeType == typeof(int) ? collection.Count : Convert.ChangeType(collection.Count, sizeType),
                            span,
                            endianness);
                        index += _bitConvert.SizeOf(sizeType);
                        var enumerator = collection.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            ConvertToBytes(enumerator.Current, span.Slice(index), endianness);
                            index += _bitConvert.SizeOf(enumerator.Current);
                        }

                        return;
                    }
                    else if (propertyValue is ITuple tuple)
                    {
                        var index = 0;
                        for (var i = 0; i < tuple.Length; i++)
                        {
                            ConvertToBytes(tuple[i], span.Slice(index), endianness);
                            index += _bitConvert.SizeOf(tuple[i]);
                        }

                        return;
                    }

                    var reflectionMetadata = _reflectionCache.GetMetadata(propertyValue.GetType());
                    if (reflectionMetadata != null)
                    {
                        InnerSerialize(reflectionMetadata, span, propertyValue);
                        return;
                    }

                    if (_customConverterFactory != null &&
                        _customConverterFactory.TryGetConverter(propertyValue.GetType(),
                            out var converter) &&
                        converter!.TryConvertToBytes(propertyValue, span, endianness))
                    {
                        return;
                    }

                    throw SerializationException.ConverterNotFoundType(propertyValue.GetType().ToString());
            }

            if (NeedReverse(endianness))
                span.Reverse();
        }
        
        
        private void InnerDeserialize(IReflectionMetadata? reflectionMetadata, in Span<byte> span, out object data,
            Type dataType)
        {
            if (reflectionMetadata != null)
            {
                data = Activator.CreateInstance(dataType) ??
                       throw SerializationException.SerializerBodyPropertyIsNull();
                foreach (var property in reflectionMetadata.Properties)
                {
                    object? propertyValue = null;
                    if (property.Attribute.PacketDataType == PacketDataType.Body)
                    {
                        if (!span.Slice(property.Attribute.Index).IsEmpty)
                        {
                            if (reflectionMetadata.BodyMetadata == null)
                            {
                                ConvertFromBytes(
                                    out propertyValue,
                                    property.PropertyType,
                                    span.Slice(property.Attribute.Index),
                                    property.Attribute.Endianness);
                            }
                            else
                            {
                                InnerDeserialize(reflectionMetadata.BodyMetadata,
                                    span.Slice(property.Attribute.Index),
                                    out propertyValue, property.PropertyType);
                            }
                        }
                    }
                    else if (property.Attribute.PacketDataType == PacketDataType.Dynamic)
                    {
                        if (!span.Slice(property.Attribute.Index).IsEmpty)
                        {
                            propertyValue = Activator.CreateInstance(property.PropertyType);
                            var index = property.Attribute.Index;
                            foreach (var propertyInfo in property.PropertyType.GetProperties())
                            {
                                ConvertFromBytes(
                                    out var pv,
                                    propertyInfo.PropertyType,
                                    span.Slice(index),
                                    property.Attribute.Endianness);

                                index += _bitConvert.SizeOf(pv);
                                propertyInfo.SetValue(propertyValue, pv);
                            }
                        }
                    }
                    else
                    {
                        if (!span.Slice(property.Attribute.Index, property.Attribute.Length).IsEmpty)
                        {
                            ConvertFromBytes(
                                out propertyValue,
                                property.PropertyType,
                                span.Slice(property.Attribute.Index, property.Attribute.Length),
                                property.Attribute.Endianness);
                        }
                    }

                    property.Set(data, propertyValue);
                }
            }
            else
            {
                ConvertFromBytes(out data, dataType, span);
            }
        }
        
          private void ConvertFromBytes(out object propertyValue, Type propertyType, in Span<byte> span,
            Endianness endianness = Endianness.LittleEndian)
        {
            if (propertyType == typeof(byte))
            {
                CheckSpanSize<byte>(span);
                propertyValue = span[0];
            }
            else if (propertyType == typeof(sbyte))
            {
                CheckSpanSize<sbyte>(span);
                propertyValue = (sbyte) span[0];
            }
            else if (propertyType == typeof(bool))
            {
                CheckSpanSize<bool>(span);
                propertyValue = span[0] > 0;
            }
            else if (propertyType == typeof(char))
            {
                CheckSpanSize<char>(span);
                propertyValue = Unsafe.ReadUnaligned<char>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(short))
            {
                CheckSpanSize<short>(span);
                propertyValue = Unsafe.ReadUnaligned<short>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(ushort))
            {
                CheckSpanSize<ushort>(span);
                propertyValue = Unsafe.ReadUnaligned<ushort>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(int))
            {
                CheckSpanSize<int>(span);
                propertyValue = Unsafe.ReadUnaligned<int>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(uint))
            {
                CheckSpanSize<uint>(span);
                propertyValue = Unsafe.ReadUnaligned<uint>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(long))
            {
                CheckSpanSize<long>(span);
                propertyValue = Unsafe.ReadUnaligned<long>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(ulong))
            {
                CheckSpanSize<ulong>(span);
                propertyValue = Unsafe.ReadUnaligned<ulong>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(float))
            {
                CheckSpanSize<float>(span);
                propertyValue = Unsafe.ReadUnaligned<float>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType == typeof(double))
            {
                CheckSpanSize<double>(span);
                propertyValue = Unsafe.ReadUnaligned<double>(ref MemoryMarshal.GetReference(span));
            }
            else if (propertyType.IsEnum)
            {
                ConvertFromBytes(out var @enum, propertyType.GetEnumUnderlyingType(), span, endianness);
                propertyValue = System.Enum.ToObject(propertyType, @enum);
            }
            else if (propertyType == typeof(string))
            {
                var stringSizeType = _options.StringSizeType;
                if (span.Length < _bitConvert.SizeOf(stringSizeType))
                    throw new ArgumentOutOfRangeException(nameof(span),
                        $"Array length lower, then size of {stringSizeType} ");

                ConvertFromBytes(out var count, stringSizeType, span, endianness);
                var stringSize = Convert.ToInt32(count);
                propertyValue = Encoding.Unicode.GetString(span.Slice(_bitConvert.SizeOf(stringSizeType), stringSize));
            }
            else
            {
                if (propertyType.GetInterfaces().Any(i => i == typeof(IList)))
                {
                    var sizeType = _options.ArrayCountType;
                    if (span.Length < _bitConvert.SizeOf(sizeType))
                        throw new ArgumentOutOfRangeException(nameof(span),
                            $"Array length lower, then size of {sizeType} ");

                    ConvertFromBytes(out var count, sizeType, span, endianness);
                    var size = Convert.ToInt32(count);

                    var underlineType = propertyType
                        .GetInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.Name.StartsWith("IList"))?
                        .GenericTypeArguments
                        .FirstOrDefault();

                    if (underlineType == null)
                        throw SerializationException.PropertyArgumentIsNull(nameof(underlineType));

                    // if (span.Length < SizeOf(sizeType) + SizeOf(underlineType) * size)
                    //     throw new ArgumentOutOfRangeException(nameof(span),
                    //         $"Array length lower, then size of {propertyType} with count {size} ");

                    propertyValue = Activator.CreateInstance(propertyType, size) ??
                                    throw SerializationException.PropertyArgumentIsNull(nameof(propertyValue));

                    var index = _bitConvert.SizeOf(sizeType);
                    for (var i = 0; i < Convert.ToInt32(count); i++)
                    {
                        ConvertFromBytes(out var item, underlineType, span.Slice(index), endianness);
                        if (((IList) propertyValue).Count > i)
                        {
                            ((IList) propertyValue)[i] = item;
                        }
                        else
                        {
                            ((IList) propertyValue).Add(item);
                        }

                        index += _bitConvert.SizeOf(item);
                    }

                    return;
                }
                else if (propertyType.GetInterfaces().Any(i => i == typeof(ITuple)))
                {
                    var types = GetTupleTypes(propertyType.GenericTypeArguments).ToList();

                    propertyValue = CreateTuple(GetTupleValues(types, span).ToList(), types);
                    return;
                }

                var reflectionMetadata = _reflectionCache.GetMetadata(propertyType);
                if (reflectionMetadata != null)
                {
                    InnerDeserialize(reflectionMetadata, span, out propertyValue, propertyType);
                    return;
                }

                if (_customConverterFactory != null &&
                    _customConverterFactory.TryGetConverter(propertyType,
                        out var converter) &&
                    converter!.TryConvertFromBytes(out propertyValue!, span, endianness))
                {
                    return;
                }
                
                throw SerializationException.ConverterNotFoundType(propertyType.ToString());
            }

            if (NeedReverse(endianness))
                span.Reverse();

            static IEnumerable<Type> GetTupleTypes(Type[] genericTypes)
            {
                foreach (var type in genericTypes)
                {
                    if (type.Name.StartsWith("ValueTuple"))
                    {
                        foreach (var subType in GetTupleTypes(type.GenericTypeArguments))
                        {
                            yield return subType;
                        }
                    }
                    else
                    {
                        yield return type;
                    }
                }
            }

            IEnumerable<object> GetTupleValues(List<Type> tupleTypes, in Span<byte> tupleSpan)
            {
                List<object> result = new(tupleTypes.Count);
                using var enumerator = tupleTypes.GetEnumerator();
                var index = 0;
                while (enumerator.MoveNext())
                {
                    ConvertFromBytes(out var item, enumerator.Current, tupleSpan.Slice(index), endianness);
                    index += _bitConvert.SizeOf(item);
                    result.Add(item);
                }

                return result;
            }
        }
        
          private static object CreateTuple(IList<object> values, IList<Type> types)
          {
              const int maxTupleMembers = 7;
              const int maxTupleArity = maxTupleMembers + 1;
              Type[] tupleTypes = new[]
              {
                  typeof(ValueTuple<>),
                  typeof(ValueTuple<,>),
                  typeof(ValueTuple<,,>),
                  typeof(ValueTuple<,,,>),
                  typeof(ValueTuple<,,,,>),
                  typeof(ValueTuple<,,,,,>),
                  typeof(ValueTuple<,,,,,,>),
                  typeof(ValueTuple<,,,,,,,>),
              };
              var numTuples = (int) Math.Ceiling((double) values.Count / maxTupleMembers);

              object? currentTuple = null;
              Type? currentTupleType = null;

              // We need to work backwards, from the last tuple
              for (var tupleIndex = numTuples - 1; tupleIndex >= 0; tupleIndex--)
              {
                  var hasRest = currentTuple != null;
                  var numTupleMembers = hasRest ? maxTupleMembers : values.Count - (maxTupleMembers * tupleIndex);
                  var tupleArity = numTupleMembers + (hasRest ? 1 : 0);

                  var typeArguments = new Type[tupleArity];
                  object[] ctorParameters = new object[tupleArity];
                  for (var i = 0; i < numTupleMembers; i++)
                  {
                      typeArguments[i] = types[tupleIndex * maxTupleMembers + i];
                      ctorParameters[i] = values[tupleIndex * maxTupleMembers + i]!;
                  }

                  if (hasRest)
                  {
                      typeArguments[^1] = currentTupleType!;
                      ctorParameters[^1] = currentTuple!;
                  }

                  currentTupleType = tupleTypes[tupleArity - 1].MakeGenericType(typeArguments);
                  currentTuple = currentTupleType.GetConstructors()[0].Invoke(ctorParameters);
              }

              return currentTuple!;
          }
          
        private bool NeedReverse(Endianness endianness) =>
            endianness == (BitConverter.IsLittleEndian ? Endianness.BigEndian : Endianness.LittleEndian);

        private void CheckSpanSize<T>(in Span<byte> span)
        {
            var size = _bitConvert.SizeOf(typeof(T));
            if (span.Length < size)
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("serializedArray",
                    $"Array length lower, then size of type {typeof(T).Name}");
        }
    }
}