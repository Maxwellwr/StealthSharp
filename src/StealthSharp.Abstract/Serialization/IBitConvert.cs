using System;

namespace StealthSharp.Serialization
{
    public interface IBitConvert
    {
        void ConvertToBytes(object propertyValue, in Span<byte> memory, Endianness endianness = Endianness.LittleEndian);

        void ConvertFromBytes(out object propertyValue, Type propertyType, in Span<byte> memory,
            Endianness endianness = Endianness.LittleEndian);
        
    }
}