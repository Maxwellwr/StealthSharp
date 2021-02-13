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
using StealthSharp.Enum;
using StealthSharp.Network;
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
            var converter = new CustomConverterFactory(spMock.Object);
            var marshaler = new Marshaler(refCache, null, converter);
            _serializer = new PacketSerializer(new Mock<IOptions<SerializationOptions>>().Object,
                refCache,  marshaler, converter);

            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_serializer, marshaler));
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
        public void Deserialize_simple_type_should_work<T>(T expected, string testValue) where T : new()
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<T>(result);

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Deserialize_complex_type_should_work(PacketHeader expected, object body, string testValue)
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length-8);
            bytes.Slice(8).CopyTo(result.Memory.Span);
            //act
            var (len, packetType, correlationId)=DeserializePacket(bytes.Slice(0, 8)); 
            var actual = _serializer.Deserialize(result, body.GetType());

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(expected.CorrelationId, correlationId);
            Assert.Equal(body, actual);
        }

        [Fact]
        public void Deserialize_Tuple_type_should_work()
        {
            //arrange
            (int, short, byte, byte[]) body = (1, 2, 3, new byte[] {4, 5, 6, 7});  
            var expected = new PacketHeader()
            {
                Length = 19,
                PacketType = PacketType.SCJournal,
                CorrelationId = 255
            };
            string testValue = "130000007B00FF00010000000200030400000004050607";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length-8);
            bytes.Slice(8).CopyTo(result.Memory.Span);
            //act
            var (len, packetType, correlationId)=DeserializePacket(bytes.Slice(0, 8)); 
            var actual = _serializer.Deserialize<(int, short, byte, byte[])>(result);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(expected.CorrelationId, correlationId);
            Assert.Equal(body.Item1, actual.Item1);
            Assert.Equal(body.Item2, actual.Item2);
            Assert.Equal(body.Item3, actual.Item3);
            Assert.Equal(body.Item4, actual.Item4);
        }

        [Fact]
        public void Deserialize_empty_body_should_work()
        {
            //arrange
            var expected = new PacketHeader()
            {
                Length = 4,
                PacketType = PacketType.SCGetStealthInfo,
                CorrelationId = 1
            };
            string testValue = "040000000C000100";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length-8);
            bytes.Slice(8).CopyTo(result.Memory.Span);
            //act
            var (len, packetType, correlationId)=DeserializePacket(bytes.Slice(0, 8));

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(expected.CorrelationId, correlationId);
        }

        [Fact]
        public void Deserialize_list_of_string_should_work()
        {
            //arrange
            var expected = new PacketHeader()
            {
                PacketType = PacketType.SCLangVersion,
                CorrelationId = 4,
                Length = 36
            };
            var body = new TypeWithListString()
            {
                ClilocID = 34578,
                Params = new List<string>() {"aa", "bb", "cc"}
            };
            string testValue = "24000000050004001287000003000000040000006100610004000000620062000400000063006300";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length-8);
            bytes.Slice(8).CopyTo(result.Memory.Span);
            //act
            var (len, packetType, correlationId)=DeserializePacket(bytes.Slice(0, 8)); 
            var actual = _serializer.Deserialize<TypeWithListString>(result);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(expected.CorrelationId, correlationId);
            Assert.Equal(body.ClilocID, actual.ClilocID);
            Assert.Equal(body.Params, actual.Params);
        }

        [Fact]
        public void Deserialize_dynamic_arrays_should_work()
        {
            //arrange
            var body = new TypeWithDynamicBody()
            {
                ClilocID = 34578,
                ExtData = new DynamicBody()
                {
                    Arr1 = new[] {1, 2},
                    Arr2 = new short[] {1, 2, 3, 4, 5},
                    Arr3 = new[]
                    {
                        new TypeWithListString()
                        {
                            ClilocID = 6,
                            Params = new List<string>() {"123"}
                        }
                    }
                }
            };
            var expected = new PacketHeader()
            {
                PacketType = PacketType.SCLangVersion,
                CorrelationId = 4,
                Length = 56
            };
            string testValue = "38000000 0500 0400 12870000 02000000 01000000 02000000 05000000 0100 0200 0300 0400 0500 01000000 06000000 01000000 06000000 3100 3200 3300";
            var bytes = FromHexString(testValue.Replace(" ",""));
            var result = new SerializationResult(bytes.Length-8);
            bytes.Slice(8).CopyTo(result.Memory.Span);
            //act
            var (len, packetType, correlationId)=DeserializePacket(bytes.Slice(0, 8)); 
            var actual = _serializer.Deserialize<TypeWithDynamicBody>(result);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(expected.CorrelationId, correlationId);
            Assert.Equal(body.ClilocID, actual.ClilocID);
            Assert.Equal(body.ExtData.Arr1, actual.ExtData.Arr1);
            Assert.Equal(body.ExtData.Arr2, actual.ExtData.Arr2);
            Assert.Equal(body.ExtData.Arr3.Length, actual.ExtData.Arr3.Length);
            Assert.Equal(body.ExtData.Arr3[0].ClilocID, actual.ExtData.Arr3[0].ClilocID);
            Assert.Equal(body.ExtData.Arr3[0].Params, actual.ExtData.Arr3[0].Params);
        }

        private static Span<byte> FromHexString(string hex) =>
            Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray()
                .AsSpan();

        private (uint len, PacketType packetType, ushort correlationId) DeserializePacket(Span<byte> bytes)
        {
            _serializer.Deserialize(bytes.Slice(0, 4), typeof(uint), out var len);
            _serializer.Deserialize(bytes.Slice(4, 2), typeof(PacketType), out var packetType);
            _serializer.Deserialize(bytes.Slice(6, 2), typeof(ushort), out var correlationId);
            if (len is null || packetType is null || correlationId is null)
                throw new NullReferenceException();
            return ((uint)len, (PacketType)packetType, (ushort)correlationId);
        }
    }
}