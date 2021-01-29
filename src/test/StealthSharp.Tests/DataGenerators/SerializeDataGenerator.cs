#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializeDataGenerator.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using StealthSharp.Network;
using StealthSharp.Serialization;

namespace StealthSharp.Tests.DataGenerators
{
    public class PacketWithTypeMapper
    {
        [PacketData(0, 4, PacketDataType = PacketDataType.Length)]
        public uint Length { get; set; }

        [PacketData(4, 2, PacketDataType = PacketDataType.TypeMapper)]
        public ushort Method { get; set; }

        [PacketData(6, 2, PacketDataType = PacketDataType.Id)]
        public ushort ReturnId { get; set; }

        [PacketData(8, PacketDataType = PacketDataType.Body)]
        public int Body { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PacketWithTypeMapper)) return false;
            return Equals((PacketWithTypeMapper) obj);
        }

        protected bool Equals(PacketWithTypeMapper other)
        {
            return Length == other.Length && Method == other.Method && ReturnId == other.ReturnId && Body == other.Body;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Length, Method, ReturnId, Body);
        }

        public static bool operator ==(PacketWithTypeMapper left, PacketWithTypeMapper right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PacketWithTypeMapper left, PacketWithTypeMapper right)
        {
            return !Equals(left, right);
        }
    }

    public class TestPacket<TId, TSize, TMapping> : IPacket<TId, TSize, TMapping>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        [PacketData(0, 4, PacketDataType = PacketDataType.Length)]
        public TSize Length { get; set; }

        [PacketData(4, 2, PacketDataType = PacketDataType.TypeMapper)]
        public TMapping TypeId { get; set; }

        [PacketData(6, 2, PacketDataType = PacketDataType.Id)]
        public TId CorrelationId { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TestPacket<TId, TSize, TMapping>)) return false;
            return Equals((TestPacket<TId, TSize, TMapping>) obj);
        }

        protected bool Equals(TestPacket<TId, TSize, TMapping> other)
        {
            return Length.Equals(other.Length) && TypeId.Equals(other.TypeId) &&
                   CorrelationId.Equals(other.CorrelationId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Length, TypeId, CorrelationId);
        }

        public static bool operator ==(TestPacket<TId, TSize, TMapping> left, TestPacket<TId, TSize, TMapping> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestPacket<TId, TSize, TMapping> left, TestPacket<TId, TSize, TMapping> right)
        {
            return !Equals(left, right);
        }
    }

    public class TestPacket<TId, TSize, TMapping, T> : TestPacket<TId, TSize, TMapping>, IPacket<TId, TSize, TMapping, T>
        where TId : unmanaged
        where TSize : unmanaged
        where TMapping : unmanaged
    {
        [PacketData(8, PacketDataType = PacketDataType.Body)]
        public T Body { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TestPacket<TId, TSize, TMapping, T>)) return false;
            return Equals((TestPacket<TId, TSize, TMapping, T>) obj);
        }

        protected bool Equals(TestPacket<TId, TSize, TMapping, T> other)
        {
            return Length.Equals(other.Length) && TypeId.Equals(other.TypeId) &&
                   CorrelationId.Equals(other.CorrelationId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Length, TypeId, CorrelationId);
        }

        public static bool operator ==(TestPacket<TId, TSize, TMapping, T> left, TestPacket<TId, TSize, TMapping, T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestPacket<TId, TSize, TMapping, T> left, TestPacket<TId, TSize, TMapping, T> right)
        {
            return !Equals(left, right);
        }
    }

    public class TestAboutData
    {
        [PacketData(0, 2)] public ushort Property1 { get; set; }
        [PacketData(2, 2)] public ushort Property2 { get; set; }
        [PacketData(4, 2)] public ushort Property3 { get; set; }

        protected bool Equals(TestAboutData other)
        {
            return Property1 == other.Property1 && Property2 == other.Property2 && Property3 == other.Property3;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TestAboutData)) return false;
            return Equals((TestAboutData) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Property1, Property2, Property3);
        }

        public static bool operator ==(TestAboutData left, TestAboutData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestAboutData left, TestAboutData right)
        {
            return !Equals(left, right);
        }
    }

    public class AboutData
    {
        [PacketData(0, 10)] public ushort[] StealthVersion { get; set; }
        [PacketData(10, 2)] public ushort Build { get; set; }
        [PacketData(12, 8)] public DateTime BuildDate { get; set; }
        [PacketData(20, 2)] public ushort GITRevNumber { get; set; }

        [PacketData(22, PacketDataType = PacketDataType.Body)]
        public string GITRevision { get; set; }

        protected bool Equals(AboutData other)
        {
            return Build == other.Build && BuildDate.Equals(other.BuildDate) && GITRevNumber == other.GITRevNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AboutData) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Build, BuildDate, GITRevNumber);
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

    public class TypeWithListString
    {
        [PacketData(0, 4)] public uint ClilocID { get; set; }

        [PacketData(4, PacketDataType = PacketDataType.Body)]
        public List<string> Params { get; set; }
    }

    public class TypeWithDynamicBody
    {
        [PacketData(0, 4)] public uint ClilocID { get; set; }

        [PacketData(4, PacketDataType = PacketDataType.Dynamic)]
        public DynamicBody ExtData { get; set; }
    }

    public class DynamicBody
    {
        public int[] Arr1 { get; set; }
        public short[] Arr2 { get; set; }
        public TypeWithListString[] Arr3 {
            get;
            set;
        }
    }

    public enum TestEnum
    {
        Value1,
        Value2
    }
    
    public class SerializeDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[]
            {
                new TestPacket<ushort, uint, ushort, byte[]>()
                {
                    Length = 13,
                    TypeId = 12,
                    CorrelationId = 1,
                    Body = new byte[] {1, 1, 1, 1, 1}
                },
                "0D0000000C000100050000000101010101"
            },
            new object[]
            {
                new TestPacket<ushort, uint, ushort, short[]>()
                {
                    Length = 14,
                    TypeId = 12,
                    CorrelationId = 1,
                    Body = new short[] {1, 1, 1}
                },
                "0E0000000C00010003000000010001000100"
            },
            new object[]
            {
                new TestPacket<ushort, uint, ushort, int>()
                {
                    Length = 8,
                    TypeId = 12,
                    CorrelationId = 1,
                    Body = 9
                },
                "080000000C00010009000000"
            },
            new object[]
            {
                new TestPacket<ushort, uint, ushort, TestAboutData>()
                {
                    Length = 10,
                    TypeId = 12,
                    CorrelationId = 1,
                    Body = new()
                    {
                        Property1 = 1,
                        Property2 = 2,
                        Property3 = 3
                    }
                },
                "0A0000000C000100010002000300"
            },
            new object[]
            {
                new TestPacket<ushort, uint, ushort, AboutData>()
                {
                    Length = 46,
                    TypeId = 12,
                    CorrelationId = 1,
                    Body = new()
                    {
                        StealthVersion = new[] {(ushort) 8, (ushort) 11, (ushort) 4},
                        Build = 0,
                        BuildDate = new DateTime(2021,1,27, 15,0,31),
                        GITRevision = "566c18d9",
                        GITRevNumber = 1422
                    }
                },
                "2E0000000C0001000300000008000B00040000004B73F002F497E5408E051000000035003600360063003100380064003900"
            },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}