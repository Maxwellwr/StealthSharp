#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IPacket.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Network
{
    public interface IPacket<TId, out TSize, out TMapping, out TBody> : IPacket<TId, TSize, TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        TBody? Body { get; }
    }

    public interface IPacket<TId, out TSize, out TMapping> : ICorrelation<TId>, ISizable<TSize>,
        ITypeMapping<TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
    }
}