#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketSerializer.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Options;

#endregion

[assembly: InternalsVisibleTo("StealthSharp.Tests")]

namespace StealthSharp.Serialization
{
    public class Marshaler : IMarshaler
    {
        private readonly SerializationOptions _options;
        private readonly IReflectionCache _reflectionCache;
        private readonly ICustomConverterFactory? _customConverterFactory;

        public Marshaler(IOptions<SerializationOptions>? options,
            IReflectionCache reflectionCache,
            ICustomConverterFactory? customConverterFactory)
        {
            _options = options?.Value ?? new SerializationOptions();
            _reflectionCache = reflectionCache ?? throw new ArgumentNullException(nameof(reflectionCache));
            _customConverterFactory =
                customConverterFactory ?? throw new ArgumentNullException(nameof(customConverterFactory));
        }

        public ISerializationResult Serialize<T>(T data)
        {
            var realLength = SizeOf(data);
            var reflectionMetadata = _reflectionCache.GetMetadata(data.GetType());
            var serializationResult = new SerializationResult(realLength);
            InnerSerialize(reflectionMetadata, serializationResult.Memory.Span, data);
            return serializationResult;
        }

        public void Serialize(in Span<byte> span, object data, Endianness endianness = Endianness.LittleEndian)
        {
            ConvertToBytes(data, span, endianness);
        }

        public object Deserialize(ISerializationResult data, Type targetType)
        {
            if (data.Memory.IsEmpty)
                throw new ArgumentOutOfRangeException(nameof(data), "Empty memory not allowed");

            var reflectionMetadata = _reflectionCache.GetMetadata(targetType);

            InnerDeserialize(reflectionMetadata, data.Memory.Span, out var deserializationResult, targetType);

            return deserializationResult;
        }

        public void Deserialize(in Span<byte> span, Type dataType, out object value,
            Endianness endianness = Endianness.LittleEndian)
        {
            ConvertFromBytes(out value, dataType, span, endianness);
        }

        private void InnerSerialize(IReflectionMetadata? reflectionMetadata, in Span<byte> span, object data)
        {
            if (reflectionMetadata != null)
            {
                var index = 0;
                foreach (var property in reflectionMetadata.Properties)
                {
                    var propertyValue = property.Get(data);
                    if (propertyValue is null) continue;
                    var size = SizeOf(propertyValue);
                    ConvertToBytes(
                        propertyValue,
                        span.Slice(index, size),
                        reflectionMetadata.Endianness);
                    index += size;
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
            object propertyValue,
            in Span<byte> span,
            Endianness endianness = Endianness.LittleEndian)
        {
            switch (propertyValue)
            {
                case byte @byte:
                    CheckSpanSize<byte>(span);
                    span[0] = @byte;
                    break;
                case sbyte @sbyte:
                    CheckSpanSize<sbyte>(span);
                    span[0] = (byte)@sbyte;
                    break;
                case bool @bool:
                    CheckSpanSize<bool>(span);
                    span[0] = @bool ? (byte)1 : (byte)0;
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
                    if (span.Length < @string.Length * 2 + SizeOf(_options.StringSizeType))
                        // ReSharper disable once NotResolvedInText
                        throw SerializationException.SpanSizeException(nameof(String));
                    ConvertToBytes(@string.Length * 2, span, GetSystemEndianness());
                    Encoding.Unicode.GetBytes(@string).CopyTo(span[4..]);
                    break;
                case Enum @enum:
                    ConvertToBytes(Convert.ChangeType(@enum, @enum.GetType().GetEnumUnderlyingType()), span,
                        GetSystemEndianness());
                    break;
                default:
                    if (propertyValue is IList collection)
                    {
                        var sizeType = _options.ArrayCountType;
                        if (span.Length < SizeOf(propertyValue))
                            throw new ArgumentOutOfRangeException(nameof(span),
                                $"Array length lower, then size of {collection.GetType().GetElementType()} array ");

                        var index = SizeOf(sizeType);
                        var itemCount = 0;
                        var enumerator = collection.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            if (enumerator.Current is null) continue;
                            ConvertToBytes(enumerator.Current, span[index..], GetSystemEndianness());
                            index += SizeOf(enumerator.Current);
                            itemCount++;
                        }

                        ConvertToBytes(
                            sizeType == typeof(int) ? itemCount : Convert.ChangeType(itemCount, sizeType),
                            span,
                            GetSystemEndianness());
                        break;
                    }
                    else if (propertyValue is ITuple tuple)
                    {
                        var index = 0;
                        for (var i = 0; i < tuple.Length; i++)
                        {
                            if (tuple[i] == null) continue;
                            var size = SizeOf(tuple[i]!);
                            ConvertToBytes(tuple[i]!, span[index..], GetSystemEndianness());
                            index += size;
                        }

                        break;
                    }

                    var reflectionMetadata = _reflectionCache.GetMetadata(propertyValue.GetType());
                    if (reflectionMetadata != null)
                    {
                        InnerSerialize(reflectionMetadata, span, propertyValue);
                        //Inner type can contain different endianness
                        return;
                    }

                    if (_customConverterFactory != null &&
                        _customConverterFactory.TryGetConverter(propertyValue.GetType(),
                            out var converter) &&
                        converter!.TryConvertToBytes(propertyValue, span, GetSystemEndianness()))
                        break;

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
                data = ActivatorHelper.CreateInstanceParameterless(dataType) ??
                       throw SerializationException.CreateInstanceException(dataType.Name);
                var index = 0;
                foreach (var property in reflectionMetadata.Properties)
                {
                    ConvertFromBytes(
                        out var propertyValue,
                        property.PropertyType,
                        span[index..],
                        reflectionMetadata.Endianness);
                    index += SizeOf(propertyValue);
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
            if (NeedReverse(endianness))
                span.Reverse();

            if (propertyType == typeof(byte))
            {
                CheckSpanSize<byte>(span);
                propertyValue = span[0];
            }
            else if (propertyType == typeof(sbyte))
            {
                CheckSpanSize<sbyte>(span);
                propertyValue = (sbyte)span[0];
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
                ConvertFromBytes(out var @enum, propertyType.GetEnumUnderlyingType(), span, GetSystemEndianness());
                propertyValue = Enum.ToObject(propertyType, @enum);
            }
            else if (propertyType == typeof(string))
            {
                var stringSizeType = _options.StringSizeType;
                if (span.Length < SizeOf(stringSizeType))
                    throw new ArgumentOutOfRangeException(nameof(span),
                        $"Array length lower, then size of {stringSizeType} ");

                ConvertFromBytes(out var count, stringSizeType, span, GetSystemEndianness());
                var stringSize = Convert.ToInt32(count);
                propertyValue = string.Concat(Encoding.Unicode.GetString(span.Slice(SizeOf(stringSizeType), stringSize)).Split('\0', StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                if (propertyType.GetInterfaces().Any(i => i == typeof(IList)))
                {
                    var sizeType = _options.ArrayCountType;
                    if (span.Length < SizeOf(sizeType))
                        throw new ArgumentOutOfRangeException(nameof(span),
                            $"Array length lower, then size of {sizeType} ");

                    ConvertFromBytes(out var count, sizeType, span, GetSystemEndianness());
                    var size = Convert.ToInt32(count);

                    var underlineType = propertyType
                        .GetInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.Name.StartsWith("IList"))?
                        .GenericTypeArguments
                        .FirstOrDefault();

                    if (underlineType == null)
                        throw SerializationException.CollectionItemTypeNotFoundException(nameof(underlineType));

                    // if (span.Length < SizeOf(sizeType) + SizeOf(underlineType) * size)
                    //     throw new ArgumentOutOfRangeException(nameof(span),
                    //         $"Array length lower, then size of {propertyType} with count {size} ");

                    propertyValue = ActivatorHelper.CreateInstance(propertyType, size) ??
                                    throw SerializationException.CreateInstanceException(nameof(propertyType));

                    var index = SizeOf(sizeType);
                    for (var i = 0; i < Convert.ToInt32(count); i++)
                    {
                        ConvertFromBytes(out var item, underlineType, span[index..], GetSystemEndianness());
                        if (((IList)propertyValue).Count > i)
                            ((IList)propertyValue)[i] = item;
                        else
                            ((IList)propertyValue).Add(item);

                        index += SizeOf(item);
                    }

                    return;
                }

                if (propertyType.GetInterfaces().Any(i => i == typeof(ITuple)))
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
                    _customConverterFactory.TryGetConverter(propertyType, out var converter) &&
                    converter!.TryConvertFromBytes(out propertyValue!, span, endianness))
                    return;

                throw SerializationException.ConverterNotFoundType(propertyType.ToString());
            }


            static IEnumerable<Type> GetTupleTypes(Type[] genericTypes)
            {
                foreach (var type in genericTypes)
                    if (type.Name.StartsWith("ValueTuple"))
                        foreach (var subType in GetTupleTypes(type.GenericTypeArguments))
                            yield return subType;
                    else
                        yield return type;
            }

            IEnumerable<object> GetTupleValues(List<Type> tupleTypes, in Span<byte> tupleSpan)
            {
                List<object> result = new(tupleTypes.Count);
                using var enumerator = tupleTypes.GetEnumerator();
                var index = 0;
                while (enumerator.MoveNext())
                {
                    ConvertFromBytes(out var item, enumerator.Current, tupleSpan[index..], endianness);
                    if (item == null)
                        throw SerializationException.NullNotSupportedException($"Tuple item at {index}");
                    index += SizeOf(item);
                    result.Add(item);
                }

                return result;
            }
        }

        public int SizeOf<T>(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            
            switch (element)
            {
                case string s:
                    return SizeOf(_options.StringSizeType) + s.Length * 2;
                case IList array:
                {
                    return array
                        .OfType<object?>()
                        .Where(e => e is not null)
                        .Aggregate(SizeOf(_options.ArrayCountType), (c, e) => c + SizeOf(e!));
                    return Enumerable.Range(0, array.Count)
                        .Where(idx => array[idx] is not null)
                        .Sum(idx => SizeOf(array[idx]!)) + SizeOf(_options.ArrayCountType);
                }
                case ITuple tuple:
                {
                    return Enumerable.Range(0, tuple.Length)
                        .Where(idx => tuple[idx] is not null)
                        .Sum(idx => SizeOf(tuple[idx]!));
                }
                default:
                {
                    var reflectionMetadata = _reflectionCache.GetMetadata(element.GetType());
                    if (reflectionMetadata != null)
                        return reflectionMetadata.Properties
                            .Where(p => p.Get(element) is not null)
                            .Sum(p => SizeOf(p.Get(element)!));

                    if (_customConverterFactory != null &&
                        _customConverterFactory.TryGetConverter(element.GetType(), out var converter))
                        return converter!.SizeOf(element);

                    return SizeOf(element.GetType());
                }
            }
        }

        /// <summary>
        /// Size of simple types like byte, int, short or Enum underlying type
        /// </summary>
        /// <param name="type">Simple type</param>
        /// <returns>Size in bytes</returns>
        public int SizeOf(Type type)
        {
            if (type == typeof(bool)) return SizeOf(typeof(byte));

            if (type.IsEnum) return SizeOf(type.GetEnumUnderlyingType());

            var reflectionMetadata = _reflectionCache.GetMetadata(type);
            if (reflectionMetadata != null)
                return reflectionMetadata.Properties.Sum(p => SizeOf(p.PropertyType));

            if (_customConverterFactory != null &&
                _customConverterFactory.TryGetConverter(type, out var converter))
                return converter!.SizeOf(type);

            return Marshal.SizeOf(type);
        }

        private static object CreateTuple(IList<object> values, IList<Type> types)
        {
            const int maxTupleMembers = 7;
            Type[] tupleTypes = new[]
            {
                typeof(ValueTuple<>),
                typeof(ValueTuple<,>),
                typeof(ValueTuple<,,>),
                typeof(ValueTuple<,,,>),
                typeof(ValueTuple<,,,,>),
                typeof(ValueTuple<,,,,,>),
                typeof(ValueTuple<,,,,,,>),
                typeof(ValueTuple<,,,,,,,>)
            };
            var numTuples = (int)Math.Ceiling((double)values.Count / maxTupleMembers);

            object? currentTuple = null;
            Type? currentTupleType = null;

            // We need to work backwards, from the last tuple
            for (var tupleIndex = numTuples - 1; tupleIndex >= 0; tupleIndex--)
            {
                var hasRest = currentTuple != null;
                var numTupleMembers = hasRest ? maxTupleMembers : values.Count - maxTupleMembers * tupleIndex;
                var tupleArity = numTupleMembers + (hasRest ? 1 : 0);

                var typeArguments = new Type[tupleArity];
                object[] ctorParameters = new object[tupleArity];
                for (var i = 0; i < numTupleMembers; i++)
                {
                    typeArguments[i] = types[tupleIndex * maxTupleMembers + i];
                    ctorParameters[i] = values[tupleIndex * maxTupleMembers + i];
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

        private static bool NeedReverse(Endianness endianness)
        {
            return endianness != GetSystemEndianness();
        }

        private static Endianness GetSystemEndianness()
        {
            return BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
        }

        private void CheckSpanSize<T>(in ReadOnlySpan<byte> span)
        {
            var size = SizeOf(typeof(T));
            if (span.Length < size)
                // ReSharper disable once NotResolvedInText
                throw SerializationException.SpanSizeException(typeof(T).Name);
        }
    }
}