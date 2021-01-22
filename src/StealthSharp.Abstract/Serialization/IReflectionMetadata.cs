using System.Collections.Generic;

namespace StealthSharp.Serialization
{
    public interface IReflectionMetadata
    {
        IReadOnlyList<PacketProperty> Properties { get; }
        int MetaLength { get; }
        int IdLength { get; }
        int LengthLength { get; }
        PacketProperty this[PacketDataType index] { get; }
        bool Contains(PacketDataType packetDataType);
        IReflectionMetadata? BodyMetadata { get; }
    }
}