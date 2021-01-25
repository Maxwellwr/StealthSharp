using System;

namespace StealthSharp.Network
{
    public interface ITypeMapper<T>
    {
        Type? GetMappedType(T typeIdentify, T requestType);
    }
}