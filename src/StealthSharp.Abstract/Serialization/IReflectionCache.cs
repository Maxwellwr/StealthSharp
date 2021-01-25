using System;

namespace StealthSharp.Serialization
{
    public interface IReflectionCache
    {
        IReflectionMetadata? GetMetadata<T>() => GetMetadata(typeof(T));
        IReflectionMetadata? GetMetadata(Type type);
    }
}