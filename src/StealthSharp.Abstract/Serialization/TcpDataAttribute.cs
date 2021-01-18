using System;

namespace StealthSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TcpDataAttribute: Attribute
    {
        /// <summary>
        /// Property position in Byte Array.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Property length in Byte Array. If TcpDataType set to TcpDataType.Body is ignored. Overwritten by the serializer.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Optional. Reverses the sequence of the elements in the serialized Byte Array.
        /// <para>Used for cases where the receiving side uses a different endianness.</para>
        /// </summary>
        public Endianness Endianness { get; set; } = Endianness.LittleEndian;

        /// <summary>
        /// Sets the serialization rule for this property.
        /// </summary>
        public TcpDataType TcpDataType { get; set; } = TcpDataType.MetaData;

        public TcpDataAttribute(int index, int length = 0)
        {
            Index = index;
            Length = length;
        }

        public override string ToString() => $"{Index.ToString()}, {Length.ToString()}, {TcpDataType.ToString()}";
    }
}