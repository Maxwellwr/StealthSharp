#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializationTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Serialization;
using StealthSharp.Tests.DataGenerators;
using Xunit;

namespace StealthSharp.Tests.Unit
{
    public class SerializationTest
    {
        private readonly IPacketSerializer _serializer;

        public SerializationTest()
        {
            var refCache = new ReflectionCache();
            var spMock = new Mock<IServiceProvider>();
            var bitConverter =new BitConvert(refCache);
            _serializer = new PacketSerializer(new Mock<IOptions<SerializationOptions>>().Object,refCache, new CustomConverterFactory(spMock.Object), bitConverter);
            
            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_serializer));

            

        }

        [Theory]
        [InlineData(1, "01000000")]
        [InlineData(1U, "01000000")]
        [InlineData(1L, "0100000000000000")]
        [InlineData((ulong) 1L, "0100000000000000")]
        [InlineData((short) 1, "0100")]
        [InlineData((ushort) 1, "0100")]
        [InlineData(true, "01")]
        [InlineData((byte) 1, "01")]
        [InlineData((sbyte) 1, "01")]
        [InlineData((PacketDataType) 1, "01")]
        public void Serialize_simple_type_should_work<T>(T testValue, string expected)
        {
            //arrange

            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Serialize_complex_type_should_work<T>(TestPacket<ushort, uint, ushort, T> testValue, string expected)
        {
            //arrange

            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_empty_body_should_work()
        {
            //arrange
            var testValue = new TestPacket<ushort, uint, ushort>()
            {
                Length = 8,
                TypeId = 12,
                CorrelationId = 1
            };
            var expected = "040000000C000100";
            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_Tuple_type_should_work()
        {
            //arrange
            var testValue = new TestPacket<ushort, uint, ushort, (int, short, byte, byte[], TestEnum)>()
            {
                TypeId = 123,
                CorrelationId = 255,
                Body = (1, 2, 3, new byte[] {4, 5, 6, 7},
                    TestEnum.Value1)
            };
            string expected = "170000007B00FF0001000000020003040000000405060700000000";
            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_with_TypeMapper_should_work()
        {
            //arrange
            var testValue = new PacketWithTypeMapper()
            {
                Length = 12,
                Method = 123,
                ReturnId = 255,
                Body = 123456789
            };
            string expected = "080000007B00FF0015CD5B07";
            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_list_of_string_should_work()
        {
            //arrange
            var testValue = new TestPacket<ushort, uint, ushort, TypeWithListString>()
            {
                TypeId = 5,
                CorrelationId = 4,
                Length = 36,
                Body = new TypeWithListString()
                {
                    ClilocID = 34578,
                    Params = new List<string>() {"aa", "bb", "cc"}
                }
            };
            string expected = "24000000050004001287000003000000040000006100610004000000620062000400000063006300";
            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_dynamic_arrays_should_work()
        {
            //arrange
            var testValue = new TestPacket<ushort, uint, ushort, TypeWithDynamicBody>()
            {
                TypeId = 5,
                CorrelationId = 4,
                Length = 56,
                Body = new TypeWithDynamicBody()
                {
                    ClilocID = 34578,
                    ExtData = new DynamicBody()
                    {
                        Arr1 = new[] {1, 2},
                        Arr2 = new short[] {1, 2, 3, 4, 5},
                        Arr3 = new []
                        {
                            new TypeWithListString()
                            {
                                ClilocID = 6, 
                                Params = new List<string>(){"123"}
                            }
                        }
                    }
                }
            };
            string expected = "38000000 0500 0400 12870000 02000000 01000000 02000000 05000000 0100 0200 0300 0400 0500 01000000 06000000 01000000 06000000 3100 3200 3300";
            //act
            var result = _serializer.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected.Replace(" ", ""), actual);
        }

        private static string ToHexString(IEnumerable<byte> array) =>
            string.Concat(array.Select(b => b.ToString("X2")));
    }
}