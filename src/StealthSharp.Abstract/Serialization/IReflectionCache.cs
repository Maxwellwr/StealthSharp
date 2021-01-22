using System;

namespace StealthSharp.Serialization
{
    public interface IReflectionCache
    {
        IReflectionMetadata? GetMetadata(Type type);
    }
}