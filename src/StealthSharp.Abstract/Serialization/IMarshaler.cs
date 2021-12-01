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
    public interface IMarshaler
    {
        ISerializationResult Serialize(object data);
        void Serialize(in Span<byte> span, object data, Endianness endianness = Endianness.LittleEndian);
        void Deserialize(in Span<byte> span, Type dataType, out object value, Endianness endianness = Endianness.LittleEndian);
        T Deserialize<T>(ISerializationResult data) => (T)Deserialize(data, typeof(T));
        object Deserialize(ISerializationResult data, Type targetType);
        
        int SizeOf(object element);

        /// <summary>
        /// Size of simple types like byte, int, short or Enum underlying type
        /// </summary>
        /// <param name="type">Simple type</param>
        /// <returns>Size in bytes</returns>
        int SizeOf(Type type);
    }
}