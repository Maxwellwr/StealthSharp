using System;

namespace StealthSharp.Serialization
{
    public interface IPacketSerializer
    {
        ISerializationResult Serialize(object data);
        T Deserialize<T>(ISerializationResult data) => (T) Deserialize(data, typeof(T));
        object Deserialize(ISerializationResult data, Type targetType);
    }
}