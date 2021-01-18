using System;
using System.Buffers;
using System.Collections.Generic;

namespace StealthSharp.Serialization
{
    public class PacketSerializer: IPacketSerializer
    {
        private readonly IReflectionCache _reflectionCache;

        public PacketSerializer(IReflectionCache reflectionCache)
        {
            _reflectionCache = reflectionCache ?? throw new ArgumentNullException(nameof(reflectionCache));
        }
        
        public IEnumerable<byte> Serialize<T>(T data)
        {
            int realLength;
            byte[] serializedBody = null;
            SerializationResult serializationResult = null;
            var examined = 0;
            ReflectionMetadata reflection = _reflectionCache.GetMetadata<T>();
            if (reflection.BodyProperty != null)
            {
                var bodyValue = reflection.BodyProperty.Get(data);
                
                if (bodyValue == null)
                    throw SerializationException.SerializerBodyPropertyIsNull();
                
                serializedBody = _bitConverter.ConvertToBytes(bodyValue, reflection.BodyProperty.PropertyType, reflection.BodyProperty.Attribute.Endianness);
                realLength = CalculateRealLength(reflection.LengthProperty, ref data, reflection.MetaLength, serializedBody.Length);
            }
            else if (reflection.ComposeProperty != null)
            {
                var composeValue = reflection.ComposeProperty.Get(data);

                if (composeValue == null)
                    throw SerializationException.SerializerComposePropertyIsNull();

                if (reflection.ComposeProperty.PropertyType.IsPrimitive)
                {
                    serializedBody = _bitConverter.ConvertToBytes(composeValue, reflection.ComposeProperty.PropertyType, reflection.ComposeProperty.Attribute.Reverse);
                    realLength = CalculateRealLength(reflection.LengthProperty, ref data, reflection.MetaLength, serializedBody.Length);
                }
                else
                {
                    serializationResult = reflection.ComposeProperty.Composition.Serialize(composeValue);
                    realLength = CalculateRealLength(reflection.LengthProperty, ref data, reflection.MetaLength, serializationResult.RealLength);   
                }
            }
            else
                realLength = reflection.MetaLength;

            var rentedArray = _byteArrayFactory(realLength);

            foreach (var property in reflection.Properties)
            {
                if (!property.PropertyType.IsPrimitive && property.Attribute.TcpDataType == TcpDataType.Compose && serializationResult != null)
                    Array.Copy(
                        serializationResult.RentedArray,
                        0,
                        rentedArray,
                        property.Attribute.Index,
                        serializationResult.RealLength
                    );
                else
                {
                    var value = property.Attribute.TcpDataType == TcpDataType.Body || property.Attribute.TcpDataType == TcpDataType.Compose
                        ? serializedBody ?? throw SerializationException.SerializerBodyPropertyIsNull()
                        : _bitConverter.ConvertToBytes(property.Get(data), property.PropertyType, property.Attribute.Reverse);

                    var valueLength = value.Length;

                    if (property.Attribute.TcpDataType != TcpDataType.Body && property.Attribute.TcpDataType != TcpDataType.Compose && valueLength > property.Attribute.Length)
                        throw SerializationException.SerializerLengthOutOfRange(property.PropertyType.ToString(), valueLength.ToString(), property.Attribute.Length.ToString());

                    value.CopyTo(rentedArray, property.Attribute.Index);
                }

                if (++examined == reflection.Properties.Count)
                    break;
            }

            return new SerializedRequest(rentedArray, realLength, serializationResult);

            static int CalculateRealLength(TcpProperty lengthProperty, ref T data, int metaLength, int dataLength)
            {
                var lengthValue = lengthProperty.PropertyType == typeof(int)
                    ? dataLength
                    : Convert.ChangeType(dataLength, lengthProperty.PropertyType);

                if (lengthProperty.IsValueType)
                    data = (TData) lengthProperty.Set(data, lengthValue);
                else
                    lengthProperty.Set(data, lengthValue);

                try
                {
                    return (int) lengthValue + metaLength;
                }
                catch (InvalidCastException)
                {
                    return Convert.ToInt32(lengthValue) + metaLength;
                }
            }
        }
    }
}