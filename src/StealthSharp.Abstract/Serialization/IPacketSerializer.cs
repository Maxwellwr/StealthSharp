using System;

namespace StealthSharp.Serialization
{
    public interface IPacketSerializer
    {
        int LengthSize { get; }
        ISerializationResult Serialize(object data);
        T Deserialize<T>(ISerializationResult data);
    }
}