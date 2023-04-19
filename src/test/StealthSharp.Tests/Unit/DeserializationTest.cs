#region Copyright

// -----------------------------------------------------------------------
// <copyright file="DeserializationTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Serialization.Converters;
using StealthSharp.Tests.DataGenerators;
using Xunit;

#endregion

namespace StealthSharp.Tests.Unit
{
    [Trait("Category", "Unit")]
    public class DeserializationTest
    {
        private readonly IMarshaler _marshaler;

        public DeserializationTest()
        {
            var refCache = new ReflectionCache();
            var spMock = new Mock<IServiceProvider>();
            var converter = new CustomConverterFactory(spMock.Object);
            _marshaler = new Marshaler(new Mock<IOptions<SerializationOptions>>().Object,
                refCache, converter);

            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_marshaler));

            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<ServerEventData>))))
                .Returns(new ServerEventDataConverter(_marshaler, refCache));
        }

        [Theory]
        [InlineData(1, "01000000")]
        [InlineData(1U, "01000000")]
        [InlineData(1L, "0100000000000000")]
        [InlineData((ulong)1L, "0100000000000000")]
        [InlineData((short)1, "0100")]
        [InlineData((ushort)1, "0100")]
        [InlineData(true, "01")]
        [InlineData((byte)1, "01")]
        [InlineData((sbyte)1, "01")]
        public void Deserialize_simple_type_should_work<T>(T expected, string testValue) where T : new()
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length);
            bytes.CopyTo(result.Memory.Span);
            //act
            var actual = _marshaler.Deserialize<T>(result);

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Deserialize_complex_type_should_work<T>(PacketHeader expected, ushort corId, T body, string testValue)
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length - 8);
            bytes[8..].CopyTo(result.Memory.Span);
            //act
            var (len, packetType) = DeserializePacket(bytes[..6]);
            _marshaler.Deserialize(bytes[6..8], typeof(ushort), out var correlationId);
            var actual = _marshaler.Deserialize<T>(result);

            //assert
            Assert.Equal(len, (uint)bytes.Length - 4);
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(typeof(ushort), correlationId.GetType());
            Assert.Equal(corId, (ushort)correlationId);
            Assert.Equal(body, actual);
        }

        [Theory]
        [ClassData(typeof(EventDataGenerator))]
        public void deserialization_server_event_data_should_work<T>(PacketHeader expected, T eventData, string testValue)
        {
            //arrange
            var bytes = FromHexString(testValue.Replace(" ", ""));
            var result = new SerializationResult(bytes.Length - 6);
            bytes[6..].CopyTo(result.Memory.Span);
            //act
            var (len, packetType) = DeserializePacket(bytes[..6]);
            var actual = _marshaler.Deserialize<T>(result);
            //assert
            Assert.Equal(len, (uint)bytes.Length - 4);
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.NotNull(actual);
            Assert.Equal(eventData, actual);
        }

        [Fact]
        public void Deserialize_Tuple_type_should_work()
        {
            //arrange
            (int, short, byte, byte[]) body = (1, 2, 3, new byte[] { 4, 5, 6, 7 });
            var expected = new PacketHeader()
            {
                Length = 19,
                PacketType = PacketType.SCJournal
            };
            var testValue = "130000007B00FF00010000000200030400000004050607";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length - 8);
            bytes[8..].CopyTo(result.Memory.Span);
            //act
            var (len, packetType) = DeserializePacket(bytes[..6]);
            _marshaler.Deserialize(bytes[6..8], typeof(ushort), out var correlationId);
            var actual = _marshaler.Deserialize<(int, short, byte, byte[])>(result);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(typeof(ushort), correlationId.GetType());
            Assert.Equal((ushort)255, (ushort)correlationId);
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
                PacketType = PacketType.SCGetStealthInfo
            };
            var testValue = "040000000C000100";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length - 8);
            bytes[8..].CopyTo(result.Memory.Span);
            //act
            var (len, packetType) = DeserializePacket(bytes[..6]);
            _marshaler.Deserialize(bytes[6..8], typeof(ushort), out var correlationId);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(typeof(ushort), correlationId.GetType());
            Assert.Equal((ushort)1, (ushort)correlationId);
        }

        [Fact]
        public void Deserialize_list_of_string_should_work()
        {
            //arrange
            var expected = new PacketHeader()
            {
                PacketType = PacketType.SCLangVersion,
                Length = 36
            };
            var body = new TypeWithListString()
            {
                ClilocId = 34578,
                Params = new List<string>() { "aa", "bb", "cc" }
            };
            const string testValue = "24000000050004001287000003000000040000006100610004000000620062000400000063006300";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult(bytes.Length - 8);
            bytes[8..].CopyTo(result.Memory.Span);
            //act
            var (len, packetType) = DeserializePacket(bytes[..6]);
            _marshaler.Deserialize(bytes[6..8], typeof(ushort), out var correlationId);
            var actual = _marshaler.Deserialize<TypeWithListString>(result);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(typeof(ushort), correlationId.GetType());
            Assert.Equal((ushort)4, (ushort)correlationId);
            Assert.Equal(body.ClilocId, actual.ClilocId);
            Assert.Equal(body.Params, actual.Params);
        }

        [Fact]
        public void Deserialize_dynamic_arrays_should_work()
        {
            //arrange
            var body = new TypeWithDynamicBody()
            {
                ClilocId = 34578,
                ExtData = new DynamicBody()
                {
                    Arr1 = new[] { 1, 2 },
                    Arr2 = new short[] { 1, 2, 3, 4, 5 },
                    Arr3 = new[]
                    {
                        new TypeWithListString()
                        {
                            ClilocId = 6,
                            Params = new List<string>() { "123" }
                        }
                    }
                }
            };
            var expected = new PacketHeader()
            {
                PacketType = PacketType.SCLangVersion,
                Length = 56
            };
            const string testValue = "38000000 0500 0400 12870000 02000000 01000000 02000000 05000000 0100 0200 0300 0400 0500 01000000 06000000 01000000 06000000 3100 3200 3300";
            var bytes = FromHexString(testValue.Replace(" ", ""));
            var result = new SerializationResult(bytes.Length - 8);
            bytes[8..].CopyTo(result.Memory.Span);
            //act
            var (len, packetType) = DeserializePacket(bytes[..6]);
            _marshaler.Deserialize(bytes[6..8], typeof(ushort), out var correlationId);
            var actual = _marshaler.Deserialize<TypeWithDynamicBody>(result);

            //assert
            Assert.Equal(expected.Length, len);
            Assert.Equal(expected.PacketType, packetType);
            Assert.Equal(typeof(ushort), correlationId.GetType());
            Assert.Equal((ushort)4, (ushort)correlationId);
            Assert.Equal(body.ClilocId, actual.ClilocId);
            Assert.Equal(body.ExtData.Arr1, actual.ExtData.Arr1);
            Assert.Equal(body.ExtData.Arr2, actual.ExtData.Arr2);
            Assert.Equal(body.ExtData.Arr3.Length, actual.ExtData.Arr3.Length);
            Assert.Equal(body.ExtData.Arr3[0].ClilocId, actual.ExtData.Arr3[0].ClilocId);
            Assert.Equal(body.ExtData.Arr3[0].Params, actual.ExtData.Arr3[0].Params);
        }

        private static Span<byte> FromHexString(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray()
                .AsSpan();
        }

        private (uint len, PacketType packetType) DeserializePacket(Span<byte> bytes)
        {
            _marshaler.Deserialize(bytes[..4], typeof(uint), out var len);
            _marshaler.Deserialize(bytes[4..6], typeof(PacketType), out var packetType);
            if (len is null || packetType is null)
                throw new NullReferenceException();
            return ((uint)len, (PacketType)packetType);
        }
    }
}