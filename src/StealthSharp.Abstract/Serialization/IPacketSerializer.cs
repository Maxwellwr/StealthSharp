#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IPacketSerializer.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Serialization
{
    public interface IPacketSerializer
    {
        ISerializationResult Serialize(object? data);
        void Serialize(in Span<byte> span, object? data, Endianness endianness = Endianness.LittleEndian);
        void Deserialize(in Span<byte> span, Type dataType, out object? value, Endianness endianness = Endianness.LittleEndian);
        T Deserialize<T>(ISerializationResult data) => (T)(Deserialize(data, typeof(T)) ?? default(T));
        object? Deserialize(ISerializationResult data, Type targetType);
    }
}