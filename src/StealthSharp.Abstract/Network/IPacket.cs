using System;

namespace StealthSharp.Network
{
    public interface IPacket<out TId, out TSize, out TMapping, out TBody> : IPacket<TId, TSize, TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        TBody Body { get; }
    }

    public interface IPacket<out TId, out TSize, out TMapping> : ICorrelation<TId>, ISizable<TSize>,
        ITypeMapping<TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
    }
}