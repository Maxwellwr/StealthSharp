using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Options;

namespace StealthSharp.Serialization
{
    public class BitConvert : IBitConvert
    {
        private readonly SerializationOptions _options;
        public BitConvert(IOptions<SerializationOptions> options)
        {
            _options = options?.Value ?? new SerializationOptions();   
        }

        public void ConvertToBytes(
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
                default:
                    if (propertyValue is IList collection)
                    {
                        var sizeType = _options.ArrayCountType;
                        if (span.Length < SizeOf(propertyValue))
                            throw new ArgumentOutOfRangeException(nameof(span),
                                $"Array length lower, then size of {collection.GetType().GetElementType()} array ");

                        var index = 0;
                        ConvertToBytes(
                            sizeType == typeof(int) ? collection.Count : Convert.ChangeType(collection.Count, sizeType),
                            span,
                            endianness);
                        index += SizeOf(sizeType);
                        var enumerator = collection.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            ConvertToBytes(enumerator.Current, span.Slice(index), endianness);
                            index += SizeOf(enumerator.Current);
                        }

                        return;
                    }
                    else if (propertyValue is ITuple tuple)
                    {
                        var index = 0;
                        for (var i = 0; i < tuple.Length; i++)
                        {
                            ConvertToBytes(tuple[i], span.Slice(index), endianness);
                            index += SizeOf(tuple[i]);
                        }

                        return;
                    }

                    throw SerializationException.ConverterNotFoundType(propertyValue.GetType().ToString());
            }

            if (NeedReverse(endianness))
                span.Reverse();
        }

        public void ConvertFromBytes(out object propertyValue, Type propertyType, in Span<byte> span,
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
            else if (propertyType == typeof(string))
            {
                propertyValue = Encoding.Unicode.GetString(span);
            }
            else
            {
                if (propertyType.GetInterfaces().Any(i => i == typeof(IList)))
                {
                    var sizeType = _options.ArrayCountType;
                    if (span.Length < SizeOf(sizeType))
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

                    if (span.Length < SizeOf(sizeType) + SizeOf(underlineType) * size)
                        throw new ArgumentOutOfRangeException(nameof(span),
                            $"Array length lower, then size of {propertyType} with count {size} ");

                    propertyValue = Activator.CreateInstance(propertyType, size) ??
                                    throw SerializationException.PropertyArgumentIsNull(nameof(propertyValue));

                    var index = SizeOf(sizeType);
                    for (var i = 0; i < Convert.ToInt32(count); i++)
                    {
                        ConvertFromBytes(out var item, underlineType, span.Slice(index), endianness);
                        ((IList) propertyValue)[i] = item;
                        index += SizeOf(item);
                    }

                    return;
                }
                else if (propertyType.GetInterfaces().Any(i => i == typeof(ITuple)))
                {
                    var types = GetTupleTypes(propertyType.GenericTypeArguments).ToList();

                    propertyValue = CreateTuple(GetTupleValues(types, span).ToList(), types);
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
                    index += SizeOf(item);
                    result.Add(item);
                }

                return result;
            }
        }

        public static int SizeOf(object? element)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is ICollection array)
            {
                var underlineType = element.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.Name.StartsWith("IList"))?
                    .GenericTypeArguments
                    .FirstOrDefault();
                if (underlineType == null)
                    throw SerializationException.ConverterNotFoundType(
                        $"Underline type of collection {element.GetType()} not found");
                return array.Count * SizeOf(underlineType) + SizeOf(typeof(int));
            }
            else if (element is ITuple tuple)
            {
                var size = 0;
                for (var i = 0; i < tuple.Length; i++)
                {
                    size += SizeOf(tuple[i]);
                }

                return size;
            }
            else
            {
                return SizeOf(element.GetType());
            }
        }

        public static int SizeOf(Type type)
        {
            if (type == typeof(bool))
            {
                return SizeOf(typeof(byte));
            }
            else
            {
                return Marshal.SizeOf(type);
            }
        }

        private bool NeedReverse(Endianness endianness) =>
            endianness == (BitConverter.IsLittleEndian ? Endianness.BigEndian : Endianness.LittleEndian);

        private void CheckSpanSize<T>(in Span<byte> span)
        {
            var size = SizeOf(typeof(T));
            if (span.Length < size)
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("serializedArray",
                    $"Array length lower, then size of type {typeof(T).Name}");
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
    }
}