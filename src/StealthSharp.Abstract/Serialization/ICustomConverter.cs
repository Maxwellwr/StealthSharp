#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="ICustomConverter.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;

namespace StealthSharp.Serialization
{
    public interface ICustomConverter<T> : ICustomConverter
    {
        bool TryConvertToBytes(T? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian)
            => TryConvertToBytes((object?)propertyValue, span, endianness);

        bool TryConvertFromBytes(out T? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian)
        {
            var result = TryConvertFromBytes(out object? pv, span, endianness);
            if (result)
                propertyValue = (T?)pv!;
            else
            {
                propertyValue = default;
            }

            return result;
        }
    }
    public interface ICustomConverter
    {
        bool TryConvertToBytes(object? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian);
        bool TryConvertFromBytes(out object? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian);
        int SizeOf(object? propertyValue);
    }
}