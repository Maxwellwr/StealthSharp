using System;
using System.Buffers;
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class TcpComposition
    {
        private readonly MethodInfo _serializerMethod;
        private readonly MethodInfo _deserializerMethod;
        private readonly FieldInfo _deserializerField;
        private readonly object _serializer;
        private readonly object _deserializer;

        public TcpComposition(Type typeData, Func<int, byte[]> byteArrayFactory, BitConverterHelper bitConverterHelper, Type typeId = null)
        {
            var serializerType = typeof(TcpSerializer<>).MakeGenericType(typeData);
            _serializerMethod = serializerType.GetMethod(nameof(TcpSerializer<TcpComposition>.Serialize));
            
            if (_serializerMethod == null)
                throw new NullReferenceException(nameof(_serializerMethod));
            
            _serializer = Activator.CreateInstance(serializerType, bitConverterHelper, byteArrayFactory);
            
            if (typeId == null)
                return;
            
            var deserializerType = typeof(TcpDeserializer<,>).MakeGenericType(typeId, typeData);
            _deserializerMethod = deserializerType.GetMethod(nameof(TcpDeserializer<int, int>.Deserialize));
            _deserializerField = typeof(ValueTuple<,>).MakeGenericType(typeId, typeData).GetField("Item2");
            
            if (_deserializerMethod == null)
                throw new NullReferenceException(nameof(_deserializerMethod));
            
            _deserializer = Activator.CreateInstance(deserializerType, bitConverterHelper);
        }

        public SerializedRequest Serialize(object data)
            => (SerializedRequest) _serializerMethod.Invoke(_serializer, new[] {data});

        public object Deserialize(in ReadOnlySequence<byte> sequence, object preKnownLength = null)
        {
            var tuple = _deserializerMethod.Invoke(_deserializer, new[] {sequence, preKnownLength});
            return _deserializerField.GetValue(tuple);
        }
    }
}