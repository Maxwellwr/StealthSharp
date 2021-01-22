using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StealthSharp.Serialization;
using Xunit;

namespace StealthSharp.Tests.DataGenerators
{

    public class Packet<T>
    {
        [PacketData(0,4, PacketDataType = PacketDataType.Length)]
        public uint Length { get; set; }
        [PacketData(4,2, PacketDataType = PacketDataType.MetaData)]
        public ushort Method { get; set; }
        [PacketData(6,2, PacketDataType = PacketDataType.Id)]
        public ushort ReturnId { get; set; }
        [PacketData(8, PacketDataType = PacketDataType.Body)]
        public T Body { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Packet<T>)) return false;
            return Equals((Packet<T>) obj);
        }

        protected bool Equals(Packet<T> other)
        {
            return Length == other.Length && Method == other.Method && ReturnId == other.ReturnId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Length, Method, ReturnId);
        }

        public static bool operator ==(Packet<T> left, Packet<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Packet<T> left, Packet<T> right)
        {
            return !Equals(left, right);
        }
    }

    public class AboutData
    {
        [PacketData(0,2)]
        public ushort Property1 { get; set; }
        [PacketData(2,2)]
        public ushort Property2 { get; set; }
        [PacketData(4,2)]
        public ushort Property3 { get; set; }

        protected bool Equals(AboutData other)
        {
            return Property1 == other.Property1 && Property2 == other.Property2 && Property3 == other.Property3;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AboutData)) return false;
            return Equals((AboutData) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Property1, Property2, Property3);
        }

        public static bool operator ==(AboutData left, AboutData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AboutData left, AboutData right)
        {
            return !Equals(left, right);
        }
    }
    public class SerializeDataGenerator:IEnumerable<object[]>
    {
            private readonly List<object[]> _data = new()
            {
                new object[]
                {
                    new Packet<byte[]>()
                    {
                        Length = 17,
                        Method = 12,
                        ReturnId = 1,
                        Body = new byte[]{1,1,1,1,1}
                    },
                    "110000000C000100050000000101010101"
                },
                new object[]
                {
                    new Packet<short[]>()
                    {
                        Length = 18,
                        Method = 12,
                        ReturnId = 1,
                        Body = new short[]{1,1,1}
                    },
                    "120000000C00010003000000010001000100"
                },
                new object[]
                {
                    new Packet<int>()
                    {
                        Length = 12,
                        Method = 12,
                        ReturnId = 1,
                        Body = 9
                    },
                    "0C0000000C00010009000000"
                },
                new object[]
                {
                    new Packet<AboutData>()
                    {
                        Length = 14,
                        Method = 12,
                        ReturnId = 1,
                        Body = new ()
                        {
                            Property1 = 1,
                            Property2 = 2,
                            Property3 = 3
                        }
                    },
                    "0E0000000C000100010002000300"
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}