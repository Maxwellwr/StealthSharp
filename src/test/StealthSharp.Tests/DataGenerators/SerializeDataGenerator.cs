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
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Tests.DataGenerators
{
 
    [Serialization.Serializable()]
    public class TypeWithListString
    {
         public uint ClilocId { get; set; }


         public List<string> Params { get; set; } = new();
    }

    [Serialization.Serializable()]
    public class TypeWithDynamicBody
    {
         public uint ClilocId { get; set; }

         public DynamicBody ExtData { get; set; } = new();
    }

    [Serialization.Serializable()]
    public class DynamicBody
    {
        public int[] Arr1 { get; set; } = Array.Empty<int>();
        public short[] Arr2 { get; set; }= Array.Empty<short>();
        public TypeWithListString[] Arr3 { get; set; } = Array.Empty<TypeWithListString>();
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
                },
                1,
                new byte[] {1, 1, 1, 1, 1},
                "0D0000000C000100050000000101010101"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 14,
                    PacketType = PacketType.SCGetStealthInfo,
                },
                1,
                new short[] {1, 1, 1},
                "0E0000000C00010003000000010001000100"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 8,
                    PacketType = PacketType.SCGetStealthInfo,
                },
                1,
                9,
                "080000000C00010009000000"
            },
            new object[]
            {
                new PacketHeader()
                {
                    Length = 46,
                    PacketType = PacketType.SCGetStealthInfo,
                },
                1,
                new AboutData()
                {
                    StealthVersion = new[] {(ushort) 8, (ushort) 11, (ushort) 4},
                    Build = 0,
                    BuildDate = new DateTime(2021,1,27, 15,0,31),
                    GitRevision = "566c18d9",
                    GitRevNumber = 1422
                },
                "2E0000000C0001000300000008000B00040000004B73F002F497E5408E051000000035003600360063003100380064003900"
            },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}