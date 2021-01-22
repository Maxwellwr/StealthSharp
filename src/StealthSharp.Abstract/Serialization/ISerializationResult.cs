using System;

namespace StealthSharp.Serialization
{
    public interface ISerializationResult:IDisposable
    {
        Memory<byte> Memory { get; }
    }
}