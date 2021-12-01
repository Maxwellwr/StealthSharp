#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializationTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections;
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

namespace StealthSharp.Tests.Unit
{
    [Trait( "Category", "Unit")]
    public class SerializationTest
    {
        private readonly IMarshaler _marshaler;

        public SerializationTest()
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
        public void Serialize_simple_type_should_work<T>(T testValue, string expected)
        {
            //arrange

            //act
            using var result = _marshaler.Serialize(testValue!);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Serialize_complex_type_should_work<T>(PacketHeader testValue, ushort corId, T body, string expected)
        {
            //arrange
            Assert.NotNull(body);
            //act
            using var result1 = _marshaler.Serialize(testValue);
            using var correlation = _marshaler.Serialize(corId);
            using var result2 = _marshaler.Serialize(body!);

            //assert
            var actual = ToHexString(result1.Memory.ToArray()) 
                         + ToHexString(correlation.Memory.ToArray())
                         + ToHexString(result2.Memory.ToArray());
            Assert.Equal(testValue.Length, (uint)result1.Length + correlation.Length + result2.Length - 4);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(EventDataGenerator))]
        public void Serialize_server_event_data_should_work<T>(PacketHeader testValue, T body, string expected)
        {
            //arrange
            Assert.NotNull(body);
            //act
            using var result1 = _marshaler.Serialize(testValue);
            using var result2 = _marshaler.Serialize(body!);

            //assert
            var actual = ToHexString(result1.Memory.ToArray()) + ToHexString(result2.Memory.ToArray());
            Assert.Equal(testValue.Length, (uint)result1.Length + result2.Length - 4);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_empty_body_should_work()
        {
            //arrange
            var testValue = new PacketHeader()
            {
                Length = 4,
                PacketType = PacketType.SCGetStealthInfo
            };
            var expected = "040000000C00";
            //act
            using var result = _marshaler.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Serialize_empty_ArrayList_should_work()
        {
            //arrange
            var testValue = new ArrayList(){null,null};
            var expected = "00000000";
            //act
            using var result = _marshaler.Serialize(testValue);

            //assert
            var actual = ToHexString(result.Memory.ToArray());
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_Tuple_type_should_work()
        {
            //arrange
            var body = (1, (short)2, (byte)3, new byte[] { 4, 5, 6, 7 }, TestEnum.Value1);
            var testValue = new PacketHeader()
            {
                Length = 0x17,
                PacketType = PacketType.SCJournal
            };
            string expected = "17000000 7B00 FF00 01000000 0200 03 04000000 04 05 06 07 00000000"
                .Replace(" ", "");
            //act
            using var result1 = _marshaler.Serialize(testValue);
            using var correlation = _marshaler.Serialize((ushort)255);
            using var result2 = _marshaler.Serialize(body);

            //assert
            var actual = ToHexString(result1.Memory.ToArray())
                         + ToHexString(correlation.Memory.ToArray())
                         + ToHexString(result2.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_list_of_string_should_work()
        {
            //arrange
            var body = new TypeWithListString()
            {
                ClilocId = 34578,
                Params = new List<string>() { "aa", "bb", "cc" }
            };
            var testValue = new PacketHeader()
            {
                PacketType = PacketType.SCLangVersion,
                Length = 36
            };
            string expected = "24000000050004001287000003000000040000006100610004000000620062000400000063006300";
            //act
            using var result1 = _marshaler.Serialize(testValue);
            using var correlation = _marshaler.Serialize((ushort)4);
            using var result2 = _marshaler.Serialize(body);

            //assert
            var actual = ToHexString(result1.Memory.ToArray()) 
                         + ToHexString(correlation.Memory.ToArray())
                         + ToHexString(result2.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_dynamic_arrays_should_work()
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
            var testValue = new PacketHeader()
            {
                PacketType = PacketType.SCLangVersion,
                Length = 56
            };
            string expected =
                "38000000 0500 0400 12870000 02000000 01000000 02000000 05000000 0100 0200 0300 0400 0500 01000000 06000000 01000000 06000000 3100 3200 3300";
            //act
            using var result1 = _marshaler.Serialize(testValue);
            using var correlation = _marshaler.Serialize((ushort)4);
            using var result2 = _marshaler.Serialize(body);

            //assert
            var actual = ToHexString(result1.Memory.ToArray()) 
                         + ToHexString(correlation.Memory.ToArray())
                         + ToHexString(result2.Memory.ToArray());
            Assert.Equal(expected.Replace(" ", ""), actual);
        }

        private static string ToHexString(IEnumerable<byte> array) =>
            string.Concat(array.Select(b => b.ToString("X2")));
    }
}