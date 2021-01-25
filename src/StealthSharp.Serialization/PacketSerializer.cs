using System;
using Microsoft.Extensions.Options;

namespace StealthSharp.Serialization
{
    public class PacketSerializer : IPacketSerializer
    {
        private readonly IReflectionCache _reflectionCache;
        private readonly IBitConvert _bitConvert;

        public PacketSerializer(IReflectionCache reflectionCache, IBitConvert bitConvert)
        {
            _reflectionCache = reflectionCache ?? throw new ArgumentNullException(nameof(reflectionCache));
            _bitConvert = bitConvert ?? throw new ArgumentNullException(nameof(bitConvert));
        }

        public ISerializationResult Serialize(object data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var reflectionMetadata = _reflectionCache.GetMetadata(data.GetType());
            var realLength = CalculateRealLength(reflectionMetadata, data);

            if (reflectionMetadata != null && reflectionMetadata.Contains(PacketDataType.Length))
            {
                var lengthProperty = reflectionMetadata[PacketDataType.Length];
                var lengthValue = lengthProperty.PropertyType == typeof(int)
                    ? realLength
                    : Convert.ChangeType(realLength, lengthProperty.PropertyType);

                lengthProperty.Set(data, lengthValue);
            }

            var serializationResult = new SerializationResult(realLength + (reflectionMetadata?.LengthLength ?? 0));

            InnerSerialize(reflectionMetadata, serializationResult.Memory, data);

            return serializationResult;
        }

        public object Deserialize(ISerializationResult data, Type targetType)
        {
            var reflectionMetadata = _reflectionCache.GetMetadata(targetType);

            InnerDeserialize(reflectionMetadata, data.Memory, out var deserializationResult, targetType);

            return deserializationResult;
        }

        private void InnerSerialize(IReflectionMetadata? reflectionMetadata, in Memory<byte> memory, object data)
        {
            if (reflectionMetadata != null)
            {
                foreach (var property in reflectionMetadata.Properties)
                {
                    var propertyValue = property.Get(data);
                    if (property.Attribute.PacketDataType == PacketDataType.Body)
                    {
                        if (propertyValue != null)
                        {
                            if (reflectionMetadata.BodyMetadata == null)
                            {
                                _bitConvert.ConvertToBytes(
                                    propertyValue,
                                    memory.Slice(property.Attribute.Index).Span,
                                    property.Attribute.Endianness);
                            }
                            else
                            {
                                InnerSerialize(reflectionMetadata.BodyMetadata, memory.Slice(property.Attribute.Index),
                                    propertyValue);
                            }
                        }
                    }
                    else
                    {
                        if (propertyValue != null)
                        {
                            _bitConvert.ConvertToBytes(
                                propertyValue,
                                memory.Slice(property.Attribute.Index, property.Attribute.Length).Span,
                                property.Attribute.Endianness);
                        }
                    }
                }
            }
            else
            {
                _bitConvert.ConvertToBytes(
                    data,
                    memory.Span);
            }
        }

        private void InnerDeserialize(IReflectionMetadata? reflectionMetadata, in Memory<byte> memory, out object data,
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
                        if (!memory.Slice(property.Attribute.Index).IsEmpty)
                        {
                            if (reflectionMetadata.BodyMetadata == null)
                            {
                                _bitConvert.ConvertFromBytes(
                                    out propertyValue,
                                    property.PropertyType,
                                    memory.Slice(property.Attribute.Index).Span,
                                    property.Attribute.Endianness);
                            }
                            else
                            {
                                InnerDeserialize(reflectionMetadata.BodyMetadata, memory.Slice(property.Attribute.Index),
                                    out propertyValue, property.PropertyType);
                            }
                        }
                    }
                    else
                    {
                        if (!memory.Slice(property.Attribute.Index, property.Attribute.Length).IsEmpty)
                        {
                            _bitConvert.ConvertFromBytes(
                                out propertyValue,
                                property.PropertyType,
                                memory.Slice(property.Attribute.Index, property.Attribute.Length).Span,
                                property.Attribute.Endianness);
                        }
                    }

                    property.Set(data, propertyValue);
                }
            }
            else
            {
                _bitConvert.ConvertFromBytes(out data, dataType, memory.Span);
            }
        }

        private int CalculateRealLength(IReflectionMetadata? refMetadata, object bodyData)
        {
            if (refMetadata == null)
                return BitConvert.SizeOf(bodyData);

            var bodySize = refMetadata.Contains(PacketDataType.Body)
                ? CalculateRealLength(refMetadata.BodyMetadata, refMetadata[PacketDataType.Body].Get(bodyData)!)
                : 0;

            var length = bodySize
                         + refMetadata.MetaLength
                         + refMetadata.IdLength
                         + refMetadata.TypeMapperLength;
            return length;
        }
    }
}