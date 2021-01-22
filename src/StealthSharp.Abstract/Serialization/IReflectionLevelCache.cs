using System;

namespace StealthSharp.Serialization
{
    public interface IReflectionLevelCache
    {
        IReflectionMetadata? GetMetadata(Type type, bool firstLevel);
    }
}