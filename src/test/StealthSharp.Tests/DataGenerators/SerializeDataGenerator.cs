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
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Tests.DataGenerators
{
    [Serialization.Serializable()]
    public class TestAboutData
    {
         public ushort Property1 { get; set; }
         public ushort Property2 { get; set; }
         public ushort Property3 { get; set; }

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

    [Serialization.Serializable()]
    public class AboutData
    {
         public ushort[] StealthVersion { get; set; }
         public ushort Build { get; set; }
         public DateTime BuildDate { get; set; }
         public ushort GITRevNumber { get; set; }
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

    [Serialization.Serializable()]
    public class TypeWithListString
    {
         public uint ClilocID { get; set; }

        
        public List<string> Params { get; set; }
    }

    [Serialization.Serializable()]
    public class TypeWithDynamicBody
    {
         public uint ClilocID { get; set; }

        public DynamicBody ExtData { get; set; }
    }

    [Serialization.Serializable()]
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
                new PacketHeader()
                {
                    Length = 13,
                    PacketType = PacketType.SCGetStealthInfo,
                    CorrelationId = 1,
                },
                new byte[] {1, 1, 1, 1, 1},
                "0D0000000C000100050000000101010101"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 14,
                    PacketType = PacketType.SCGetStealthInfo,
                    CorrelationId = 1,
                },
                new short[] {1, 1, 1},
                "0E0000000C00010003000000010001000100"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 8,
                    PacketType = PacketType.SCGetStealthInfo,
                    CorrelationId = 1,
                },
                9,
                "080000000C00010009000000"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 10,
                    PacketType = PacketType.SCGetStealthInfo,
                    CorrelationId = 1,
                },
                new TestAboutData()
                {
                    Property1 = 1,
                    Property2 = 2,
                    Property3 = 3
                },
                "0A0000000C000100010002000300"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 46,
                    PacketType = PacketType.SCGetStealthInfo,
                    CorrelationId = 1,
                },
                new AboutData()
                {
                    StealthVersion = new[] {(ushort) 8, (ushort) 11, (ushort) 4},
                    Build = 0,
                    BuildDate = new DateTime(2021,1,27, 15,0,31),
                    GITRevision = "566c18d9",
                    GITRevNumber = 1422
                },
                "2E0000000C0001000300000008000B00040000004B73F002F497E5408E051000000035003600360063003100380064003900"
            },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}