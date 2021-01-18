using System.Collections.Generic;

namespace StealthSharp.Serialization
{
    public interface IPacketSerializer
    {
        IEnumerable<byte> Serialize<T>(T data);
    }
}