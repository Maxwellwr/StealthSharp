#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Marshaler.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Options;

#endregion

[assembly: InternalsVisibleTo("StealthSharp.Tests")]

namespace StealthSharp.Serialization
{
    /// <summary>
    ///     Reflection based binary (de)serializer. Everything that can be worked out from a <see cref="Type" />
    ///     (kind, fixed size, converter, factory, list element type) is computed once and cached in
    ///     <see cref="TypeInfo" />, so the per value work is just a switch and a few reads or writes.
    /// </summary>
    public class Marshaler : IMarshaler
    {
        private readonly SerializationOptions _options;
        private readonly IReflectionCache _reflectionCache;
        private readonly ICustomConverterFactory? _customConverterFactory;
        private readonly ConcurrentDictionary<Type, TypeInfo> _typeInfos = new();
        private readonly ConcurrentDictionary<Type, int> _typeSizes = new();
        private readonly int _arrayCountSize;
        private readonly int _stringSizeSize;

        public Marshaler(IOptions<SerializationOptions>? options,
            IReflectionCache reflectionCache,
            ICustomConverterFactory? customConverterFactory)
        {
            _options = options?.Value ?? new SerializationOptions();
            _reflectionCache = reflectionCache ?? throw new ArgumentNullException(nameof(reflectionCache));
            _customConverterFactory =
                customConverterFactory ?? throw new ArgumentNullException(nameof(customConverterFactory));
            _arrayCountSize = SizeOf(_options.ArrayCountType);
            _stringSizeSize = SizeOf(_options.StringSizeType);
        }

        public ISerializationResult Serialize<T>(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var serializationResult = new SerializationResult(SizeOfValue(data));
            try
            {
                Write(data, serializationResult.Memory.Span, Endianness.LittleEndian);
            }
            catch
            {
                serializationResult.Dispose();
                throw;
            }

            return serializationResult;
        }

        public void Serialize(in Span<byte> span, object data, Endianness endianness = Endianness.LittleEndian)
        {
            Write(data, span, endianness);
        }

        public object Deserialize(ISerializationResult data, Type targetType)
        {
            if (data.Memory.IsEmpty)
                throw new ArgumentOutOfRangeException(nameof(data), "Empty memory not allowed");

            return Read(targetType, data.Memory.Span, Endianness.LittleEndian, out _);
        }

        public void Deserialize(in Span<byte> span, Type dataType, out object value,
            Endianness endianness = Endianness.LittleEndian)
        {
            value = Read(dataType, span, endianness, out _);
        }

        public int SizeOf<T>(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            return SizeOfValue(element);
        }

        /// <summary>
        ///     Size of simple types like byte, int, short or Enum underlying type
        /// </summary>
        /// <param name="type">Simple type</param>
        /// <returns>Size in bytes</returns>
        public int SizeOf(Type type)
        {
            return _typeSizes.GetOrAdd(type, static (t, self) => self.ComputeSize(t), this);
        }

        private int ComputeSize(Type type)
        {
            if (type == typeof(bool)) return ComputeSize(typeof(byte));

            if (type.IsEnum) return SizeOf(type.GetEnumUnderlyingType());

            var reflectionMetadata = _reflectionCache.GetMetadata(type);
            if (reflectionMetadata != null)
                return reflectionMetadata.Properties.Sum(p => SizeOf(p.PropertyType));

            if (_customConverterFactory != null &&
                _customConverterFactory.TryGetConverter(type, out var converter))
                return converter!.SizeOf(type);

            return Marshal.SizeOf(type);
        }

        private int SizeOfValue(object element)
        {
            var info = GetTypeInfo(element.GetType());
            switch (info.Kind)
            {
                case Kind.String:
                    return _stringSizeSize + ((string)element).Length * 2;
                case Kind.List:
                {
                    if (info.IsPrimitiveArray)
                        return _arrayCountSize + ((Array)element).Length * info.ElementSize;

                    var list = (IList)element;
                    var size = _arrayCountSize;
                    for (var i = 0; i < list.Count; i++)
                        if (list[i] is { } item)
                            size += SizeOfValue(item);
                    return size;
                }
                case Kind.Tuple:
                {
                    var tuple = (ITuple)element;
                    var size = 0;
                    for (var i = 0; i < tuple.Length; i++)
                        if (tuple[i] is { } item)
                            size += SizeOfValue(item);
                    return size;
                }
                case Kind.Object:
                {
                    var properties = info.Metadata!.Properties;
                    var size = 0;
                    for (var i = 0; i < properties.Count; i++)
                        if (properties[i].Get(element) is { } value)
                            size += SizeOfValue(value);
                    return size;
                }
                case Kind.Converter:
                    return info.Converter!.SizeOf(element);
                case Kind.Unsupported:
                    return SizeOf(element.GetType());
                default:
                    return info.FixedSize;
            }
        }

        /// <summary>Writes <paramref name="value" /> to the start of <paramref name="span" />, returns bytes written.</summary>
        private int Write(object value, Span<byte> span, Endianness endianness)
        {
            var info = GetTypeInfo(value.GetType());
            int written;
            switch (info.Kind)
            {
                case Kind.Byte:
                    written = WritePrimitive(span, info, (byte)value);
                    break;
                case Kind.SByte:
                    written = WritePrimitive(span, info, (sbyte)value);
                    break;
                case Kind.Bool:
                    written = WritePrimitive(span, info, (bool)value ? (byte)1 : (byte)0);
                    break;
                case Kind.Char:
                    written = WritePrimitive(span, info, (char)value);
                    break;
                case Kind.Double:
                    written = WritePrimitive(span, info, (double)value);
                    break;
                case Kind.Short:
                    written = WritePrimitive(span, info, (short)value);
                    break;
                case Kind.UShort:
                    written = WritePrimitive(span, info, (ushort)value);
                    break;
                case Kind.Int:
                    written = WritePrimitive(span, info, (int)value);
                    break;
                case Kind.UInt:
                    written = WritePrimitive(span, info, (uint)value);
                    break;
                case Kind.Long:
                    written = WritePrimitive(span, info, (long)value);
                    break;
                case Kind.ULong:
                    written = WritePrimitive(span, info, (ulong)value);
                    break;
                case Kind.Float:
                    written = WritePrimitive(span, info, (float)value);
                    break;
                case Kind.Enum:
                    written = WriteEnum((Enum)value, span, info);
                    break;
                case Kind.String:
                {
                    var text = (string)value;
                    if (span.Length < text.Length * 2 + _stringSizeSize)
                        // ReSharper disable once NotResolvedInText
                        throw SerializationException.SpanSizeException(nameof(String));
                    Write(text.Length * 2, span, GetSystemEndianness());
                    written = _stringSizeSize + Encoding.Unicode.GetBytes(text, span[4..]);
                    break;
                }
                case Kind.List:
                    written = WriteList(value, span, info);
                    break;
                case Kind.Tuple:
                {
                    var tuple = (ITuple)value;
                    written = 0;
                    for (var i = 0; i < tuple.Length; i++)
                        if (tuple[i] is { } item)
                            written += Write(item, span[written..], GetSystemEndianness());
                    break;
                }
                case Kind.Object:
                {
                    var properties = info.Metadata!.Properties;
                    var propertyEndianness = info.Metadata.Endianness;
                    written = 0;
                    for (var i = 0; i < properties.Count; i++)
                        if (properties[i].Get(value) is { } propertyValue)
                            written += Write(propertyValue, span[written..], propertyEndianness);
                    // Inner type can contain different endianness
                    return written;
                }
                case Kind.Converter:
                {
                    // Converters (e.g. the event one) expect a span of exactly the size of the value.
                    written = info.Converter!.SizeOf(value);
                    if (!info.Converter.TryConvertToBytes(value, span[..written], GetSystemEndianness()))
                        throw SerializationException.ConverterNotFoundType(value.GetType().ToString());
                    break;
                }
                default:
                    throw SerializationException.ConverterNotFoundType(value.GetType().ToString());
            }

            if (NeedReverse(endianness))
                span[..written].Reverse();
            return written;
        }

        private static int WritePrimitive<T>(Span<byte> span, TypeInfo info, T value) where T : unmanaged
        {
            if (span.Length < info.FixedSize)
                throw SerializationException.SpanSizeException(typeof(T).Name);
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);
            return info.FixedSize;
        }

        private static int WriteEnum(Enum value, Span<byte> span, TypeInfo info)
        {
            switch (info.EnumTypeCode)
            {
                case TypeCode.Byte: return WritePrimitive(span, info, Convert.ToByte(value));
                case TypeCode.SByte: return WritePrimitive(span, info, Convert.ToSByte(value));
                case TypeCode.Int16: return WritePrimitive(span, info, Convert.ToInt16(value));
                case TypeCode.UInt16: return WritePrimitive(span, info, Convert.ToUInt16(value));
                case TypeCode.Int32: return WritePrimitive(span, info, Convert.ToInt32(value));
                case TypeCode.UInt32: return WritePrimitive(span, info, Convert.ToUInt32(value));
                case TypeCode.Int64: return WritePrimitive(span, info, Convert.ToInt64(value));
                case TypeCode.UInt64: return WritePrimitive(span, info, Convert.ToUInt64(value));
                default: throw SerializationException.ConverterNotFoundType(value.GetType().ToString());
            }
        }

        private int WriteList(object value, Span<byte> span, TypeInfo info)
        {
            if (info.IsPrimitiveArray)
            {
                var array = (Array)value;
                var dataSize = array.Length * info.ElementSize;
                if (span.Length < _arrayCountSize + dataSize)
                    throw new ArgumentOutOfRangeException(nameof(span),
                        $"Array length lower, then size of {info.ElementType} array ");

                WriteCount(span, array.Length);
                CopyArrayTo(array, span[_arrayCountSize..]);
                return _arrayCountSize + dataSize;
            }

            var list = (IList)value;
            var index = _arrayCountSize;
            var itemCount = 0;
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] is not { } item) continue;
                index += Write(item, span[index..], GetSystemEndianness());
                itemCount++;
            }

            WriteCount(span, itemCount);
            return index;
        }

        private void WriteCount(Span<byte> span, int count)
        {
            var type = _options.ArrayCountType;
            if (type == typeof(uint))
                WritePrimitive(span, GetTypeInfo(type), (uint)count);
            else if (type == typeof(int))
                WritePrimitive(span, GetTypeInfo(type), count);
            else
                Write(Convert.ChangeType(count, type), span, GetSystemEndianness());
        }

        private static void CopyArrayTo(Array array, Span<byte> destination)
        {
            switch (array)
            {
                case byte[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case sbyte[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case short[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case ushort[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case int[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case uint[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case long[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case ulong[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case float[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                case double[] a: MemoryMarshal.AsBytes(a.AsSpan()).CopyTo(destination); break;
                default: throw new InvalidOperationException($"Not a primitive array: {array.GetType()}");
            }
        }

        /// <summary>Reads a value of <paramref name="type" /> from the start of <paramref name="span" />.</summary>
        /// <param name="consumed">Bytes the value occupies in <paramref name="span" />.</param>
        private object Read(Type type, Span<byte> span, Endianness endianness, out int consumed)
        {
            if (NeedReverse(endianness))
                span.Reverse();

            var info = GetTypeInfo(type);
            switch (info.Kind)
            {
                case Kind.Byte:
                    return ReadPrimitive<byte>(span, info, out consumed);
                case Kind.SByte:
                    return ReadPrimitive<sbyte>(span, info, out consumed);
                case Kind.Bool:
                    return ReadPrimitive<byte>(span, info, out consumed) > 0;
                case Kind.Char:
                    return ReadPrimitive<char>(span, info, out consumed);
                case Kind.Short:
                    return ReadPrimitive<short>(span, info, out consumed);
                case Kind.UShort:
                    return ReadPrimitive<ushort>(span, info, out consumed);
                case Kind.Int:
                    return ReadPrimitive<int>(span, info, out consumed);
                case Kind.UInt:
                    return ReadPrimitive<uint>(span, info, out consumed);
                case Kind.Long:
                    return ReadPrimitive<long>(span, info, out consumed);
                case Kind.ULong:
                    return ReadPrimitive<ulong>(span, info, out consumed);
                case Kind.Float:
                    return ReadPrimitive<float>(span, info, out consumed);
                case Kind.Double:
                    return ReadPrimitive<double>(span, info, out consumed);
                case Kind.Enum:
                {
                    var underlying = Read(info.EnumUnderlyingType!, span, GetSystemEndianness(), out consumed);
                    return Enum.ToObject(type, underlying);
                }
                case Kind.String:
                    return ReadString(span, out consumed);
                case Kind.List:
                    return ReadList(type, info, span, out consumed);
                case Kind.Tuple:
                    return ReadTuple(info, span, endianness, out consumed);
                case Kind.Object:
                {
                    var metadata = info.Metadata!;
                    var properties = metadata.Properties;
                    var data = info.CreateInstance();
                    var index = 0;
                    for (var i = 0; i < properties.Count; i++)
                    {
                        var property = properties[i];
                        var propertyValue = Read(property.PropertyType, span[index..], metadata.Endianness, out var size);
                        index += size;
                        property.Set(data, propertyValue);
                    }

                    consumed = index;
                    return data;
                }
                case Kind.Converter:
                {
                    if (!info.Converter!.TryConvertFromBytes(out var converted, span, endianness))
                        throw SerializationException.ConverterNotFoundType(type.ToString());
                    consumed = SizeOfValue(converted!);
                    return converted!;
                }
                default:
                    throw SerializationException.ConverterNotFoundType(type.ToString());
            }
        }

        private static T ReadPrimitive<T>(Span<byte> span, TypeInfo info, out int consumed) where T : unmanaged
        {
            if (span.Length < info.FixedSize)
                throw SerializationException.SpanSizeException(typeof(T).Name);
            consumed = info.FixedSize;
            return Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(span));
        }

        private string ReadString(Span<byte> span, out int consumed)
        {
            var stringSizeType = _options.StringSizeType;
            if (span.Length < _stringSizeSize)
                throw new ArgumentOutOfRangeException(nameof(span), $"Array length lower, then size of {stringSizeType} ");

            var stringSize = ReadCount(stringSizeType, span);
            var text = Encoding.Unicode.GetString(span.Slice(_stringSizeSize, stringSize));
            consumed = _stringSizeSize + stringSize;
            return text.Contains('\0') ? text.Replace("\0", string.Empty) : text;
        }

        private int ReadCount(Type countType, Span<byte> span)
        {
            if (countType == typeof(uint))
                return checked((int)ReadPrimitive<uint>(span, GetTypeInfo(countType), out _));
            if (countType == typeof(int))
                return ReadPrimitive<int>(span, GetTypeInfo(countType), out _);

            return Convert.ToInt32(Read(countType, span, GetSystemEndianness(), out _));
        }

        private object ReadList(Type type, TypeInfo info, Span<byte> span, out int consumed)
        {
            var sizeType = _options.ArrayCountType;
            if (span.Length < _arrayCountSize)
                throw new ArgumentOutOfRangeException(nameof(span), $"Array length lower, then size of {sizeType} ");

            var count = ReadCount(sizeType, span);
            var elementType = info.ElementType ??
                              throw SerializationException.CollectionItemTypeNotFoundException("underlineType");

            if (info.IsPrimitiveArray)
            {
                var dataSize = (long)count * info.ElementSize;
                if (span.Length - _arrayCountSize < dataSize)
                    throw SerializationException.SpanSizeException(elementType.Name);

                consumed = _arrayCountSize + (int)dataSize;
                return CreateArray(elementType, count, span.Slice(_arrayCountSize, (int)dataSize));
            }

            var list = info.CreateList(count);
            var index = _arrayCountSize;
            for (var i = 0; i < count; i++)
            {
                var item = Read(elementType, span[index..], GetSystemEndianness(), out var size);
                if (list.Count > i)
                    list[i] = item;
                else
                    list.Add(item);

                index += size;
            }

            consumed = index;
            return list;
        }

        private static Array CreateArray(Type elementType, int count, Span<byte> source)
        {
            switch (Type.GetTypeCode(elementType))
            {
                case TypeCode.Byte: return Copy<byte>(count, source);
                case TypeCode.SByte: return Copy<sbyte>(count, source);
                case TypeCode.Int16: return Copy<short>(count, source);
                case TypeCode.UInt16: return Copy<ushort>(count, source);
                case TypeCode.Int32: return Copy<int>(count, source);
                case TypeCode.UInt32: return Copy<uint>(count, source);
                case TypeCode.Int64: return Copy<long>(count, source);
                case TypeCode.UInt64: return Copy<ulong>(count, source);
                case TypeCode.Single: return Copy<float>(count, source);
                case TypeCode.Double: return Copy<double>(count, source);
                default: throw new InvalidOperationException($"Not a primitive element type: {elementType}");
            }

            static T[] Copy<T>(int count, Span<byte> source) where T : unmanaged
            {
                var array = new T[count];
                source.CopyTo(MemoryMarshal.AsBytes(array.AsSpan()));
                return array;
            }
        }

        private object ReadTuple(TypeInfo info, Span<byte> span, Endianness endianness, out int consumed)
        {
            var types = info.TupleTypes!;
            var values = new List<object>(types.Length);
            var index = 0;
            foreach (var itemType in types)
            {
                var item = Read(itemType, span[index..], endianness, out var size);
                index += size;
                values.Add(item);
            }

            consumed = index;
            return CreateTuple(values, types);
        }

        private TypeInfo GetTypeInfo(Type type)
        {
            return _typeInfos.GetOrAdd(type, static (t, self) => self.CreateTypeInfo(t), this);
        }

        private TypeInfo CreateTypeInfo(Type type)
        {
            var info = new TypeInfo(type);
            if (PrimitiveKinds.TryGetValue(type, out var primitiveKind))
            {
                info.Kind = primitiveKind;
                info.FixedSize = ComputeSize(type);
            }
            else if (type.IsEnum)
            {
                info.Kind = Kind.Enum;
                info.EnumUnderlyingType = type.GetEnumUnderlyingType();
                info.EnumTypeCode = Type.GetTypeCode(info.EnumUnderlyingType);
                info.FixedSize = ComputeSize(type);
            }
            else if (type == typeof(string))
            {
                info.Kind = Kind.String;
            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                info.Kind = Kind.List;
                info.ElementType = type
                    .GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.Name.StartsWith("IList"))?
                    .GenericTypeArguments
                    .FirstOrDefault();
                if (type.IsArray && info.ElementType is not null && BulkCopyTypes.Contains(info.ElementType) &&
                    BitConverter.IsLittleEndian)
                {
                    info.IsPrimitiveArray = true;
                    info.ElementSize = ComputeSize(info.ElementType);
                }
            }
            else if (typeof(ITuple).IsAssignableFrom(type))
            {
                info.Kind = Kind.Tuple;
                info.TupleTypes = GetTupleTypes(type.GenericTypeArguments).ToArray();
            }
            else if (_reflectionCache.GetMetadata(type) is { } metadata)
            {
                info.Kind = Kind.Object;
                info.Metadata = metadata;
            }
            else if (_customConverterFactory != null &&
                     _customConverterFactory.TryGetConverter(type, out var converter))
            {
                info.Kind = Kind.Converter;
                info.Converter = converter;
            }
            else
            {
                info.Kind = Kind.Unsupported;
            }

            return info;
        }

        private static IEnumerable<Type> GetTupleTypes(Type[] genericTypes)
        {
            foreach (var type in genericTypes)
                if (type.Name.StartsWith("ValueTuple"))
                    foreach (var subType in GetTupleTypes(type.GenericTypeArguments))
                        yield return subType;
                else
                    yield return type;
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

        private static readonly Dictionary<Type, Kind> PrimitiveKinds = new()
        {
            [typeof(byte)] = Kind.Byte,
            [typeof(sbyte)] = Kind.SByte,
            [typeof(bool)] = Kind.Bool,
            [typeof(char)] = Kind.Char,
            [typeof(double)] = Kind.Double,
            [typeof(short)] = Kind.Short,
            [typeof(ushort)] = Kind.UShort,
            [typeof(int)] = Kind.Int,
            [typeof(uint)] = Kind.UInt,
            [typeof(long)] = Kind.Long,
            [typeof(ulong)] = Kind.ULong,
            [typeof(float)] = Kind.Float
        };

        /// <summary>Element types whose arrays have the same layout in memory and on the wire.</summary>
        private static readonly HashSet<Type> BulkCopyTypes = new()
        {
            typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long),
            typeof(ulong), typeof(float), typeof(double)
        };

        private enum Kind
        {
            Unsupported,
            Byte,
            SByte,
            Bool,
            Char,
            Double,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong,
            Float,
            Enum,
            String,
            List,
            Tuple,
            Object,
            Converter
        }

        private sealed class TypeInfo
        {
            private Func<object>? _factory;
            private ConstructorInfo? _capacityConstructor;

            public TypeInfo(Type type)
            {
                Type = type;
            }

            public Type Type { get; }
            public Kind Kind { get; set; }

            /// <summary>Size of primitives and enums, as <see cref="Marshaler.SizeOf(Type)" /> reports it.</summary>
            public int FixedSize { get; set; }

            public Type? EnumUnderlyingType { get; set; }
            public TypeCode EnumTypeCode { get; set; }
            public IReflectionMetadata? Metadata { get; set; }
            public ICustomConverter? Converter { get; set; }
            public Type? ElementType { get; set; }
            public int ElementSize { get; set; }
            public bool IsPrimitiveArray { get; set; }
            public Type[]? TupleTypes { get; set; }

            public object CreateInstance()
            {
                if (_factory is null)
                {
                    var constructor = Type.GetConstructor(Type.EmptyTypes);
                    _factory = constructor is not null
                        ? Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(constructor), typeof(object)))
                            .Compile()
                        : () => ActivatorHelper.CreateInstanceParameterless(Type);
                }

                return _factory();
            }

            public IList CreateList(int count)
            {
                if (Type.IsArray)
                    return Array.CreateInstance(ElementType!, count);

                _capacityConstructor ??= Type.GetConstructor(new[] { typeof(int) }) ??
                                         throw SerializationException.CreateInstanceException(nameof(Type));
                return (IList)_capacityConstructor.Invoke(new object[] { count });
            }
        }
    }
}
