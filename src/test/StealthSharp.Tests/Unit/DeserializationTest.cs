#region Copyright

// -----------------------------------------------------------------------
// <copyright file="DeserializationTest.cs" company="StealthSharp">
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
    public class DeserializationTest
    {
        private readonly IPacketSerializer _serializer;

        public DeserializationTest()
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
        public void Deserialize_simple_type_should_work<T>(T expected, string testValue) where T : new()
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<T>(result);

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Deserialize_complex_type_should_work<T>(TestPacket<ushort, uint, ushort, T> expected, string testValue)
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<TestPacket<ushort, uint, ushort, T>>(result);

            //assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Body, actual.Body);
        }

        [Fact]
        public void Deserialize_Tuple_type_should_work()
        {
            //arrange
            var expected = new TestPacket<ushort, uint, ushort, (int, short, byte, byte[])>()
            {
                Length = 19,
                TypeId = 123,
                CorrelationId = 255,
                Body = (1, 2, 3, new byte[] {4, 5, 6, 7})
            };
            string testValue = "130000007B00FF00010000000200030400000004050607";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<TestPacket<ushort, uint, ushort, (int, short, byte, byte[])>>(result);

            //assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.TypeId, actual.TypeId);
            Assert.Equal(expected.CorrelationId, actual.CorrelationId);
            Assert.Equal(expected.Body.Item1, actual.Body.Item1);
            Assert.Equal(expected.Body.Item2, actual.Body.Item2);
            Assert.Equal(expected.Body.Item3, actual.Body.Item3);
            Assert.Equal(expected.Body.Item4, actual.Body.Item4);
        }

        [Fact]
        public void Deserialize_empty_body_should_work()
        {
            //arrange
            var expected = new TestPacket<ushort, uint, ushort>()
            {
                Length = 4,
                TypeId = 12,
                CorrelationId = 1
            };
            string testValue = "040000000C000100";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<TestPacket<ushort, uint, ushort>>(result);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Deserialize_with_TypeMapper_should_work()
        {
            //arrange
            var expected = new PacketWithTypeMapper()
            {
                Length = 8,
                Method = 123,
                ReturnId = 255,
                Body = 123456789
            };
            string testValue = "080000007B00FF0015CD5B07";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<PacketWithTypeMapper>(result);

            //assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.Method, actual.Method);
            Assert.Equal(expected.ReturnId, actual.ReturnId);
            Assert.Equal(expected.Body, actual.Body);
        }

        [Fact]
        public void Deserialize_list_of_string_should_work()
        {
            //arrange
            var expected = new TestPacket<ushort, uint, ushort, TypeWithListString>()
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
            string testValue = "24000000050004001287000003000000040000006100610004000000620062000400000063006300";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<TestPacket<ushort, uint, ushort, TypeWithListString>>(result);

            //assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Body.ClilocID, actual.Body.ClilocID);
            Assert.Equal(expected.Body.Params, actual.Body.Params);
        }

        [Fact]
        public void Deserialize_dynamic_arrays_should_work()
        {
            //arrange
            var expected = new TestPacket<ushort, uint, ushort, TypeWithDynamicBody>()
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
            string testValue = "38000000 0500 0400 12870000 02000000 01000000 02000000 05000000 0100 0200 0300 0400 0500 01000000 06000000 01000000 06000000 3100 3200 3300";
            var bytes = FromHexString(testValue.Replace(" ",""));
            var result = new SerializationResult(bytes.Length);
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<TestPacket<ushort, uint, ushort, TypeWithDynamicBody>>(result);

            //assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Body.ClilocID, actual.Body.ClilocID);
            Assert.Equal(expected.Body.ExtData.Arr1, actual.Body.ExtData.Arr1);
            Assert.Equal(expected.Body.ExtData.Arr2, actual.Body.ExtData.Arr2);
            Assert.Equal(expected.Body.ExtData.Arr3.Length, actual.Body.ExtData.Arr3.Length);
            Assert.Equal(expected.Body.ExtData.Arr3[0].ClilocID, actual.Body.ExtData.Arr3[0].ClilocID);
            Assert.Equal(expected.Body.ExtData.Arr3[0].Params, actual.Body.ExtData.Arr3[0].Params);
        }

        private static byte[] FromHexString(string hex) =>
            Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
    }
}