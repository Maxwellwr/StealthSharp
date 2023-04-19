#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="MarshalerTest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Enumeration;
using StealthSharp.Serialization;
using StealthSharp.Serialization.Converters;
using StealthSharp.Tests.Model;
using Xunit;

#endregion

namespace StealthSharp.Tests.Unit
{
    [Trait("Category", "Unit")]
    public class SizeOfTest
    {
        private readonly IMarshaler _marshaler;

        public SizeOfTest()
        {
            var refCache = new ReflectionCache();
            var spMock = new Mock<IServiceProvider>();
            var converter = new CustomConverterFactory(spMock.Object);
            _marshaler = new Marshaler(new Mock<IOptions<SerializationOptions>>().Object,
                refCache, converter);
            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_marshaler));
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(1U, 4)]
        [InlineData(1L, 8)]
        [InlineData((ulong)1L, 8)]
        [InlineData((short)1, 2)]
        [InlineData((ushort)1, 2)]
        [InlineData(true, 1)]
        [InlineData((byte)1, 1)]
        [InlineData((sbyte)1, 1)]
        [InlineData(PacketType.SCAttack, 2)]
        [InlineData("null", 12)]
        public void SizeOf_on_simple_type_should_work<T>(T data, int expected)
        {
            // arrange
            // act
            var actual = _marshaler.SizeOf(data!);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SizeOf_on_arrays_type_should_work()
        {
            // arrange
            var array = new[] { (ushort)1, (ushort)2, (ushort)3, (ushort)4, (ushort)5 };
            var expected = 14;
            // act
            var actual = _marshaler.SizeOf(array);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SizeOf_on_ListOfStrings_type_should_work()
        {
            // arrange
            var array = new List<string> { "123", "456", "123456" };
            var expected = 40;
            // act
            var actual = _marshaler.SizeOf(array);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SizeOf_on_ArrayList_should_return_only_size()
        {
            // arrange
            var data = new ArrayList();
            // act
            var actual = _marshaler.SizeOf(data);
            // assert
            Assert.Equal(4, actual);
        }

        [Fact]
        public void SizeOf_on_Tuple_type_should_work()
        {
            // arrange
            var data = (4, (ushort)2, (byte)1, "6");
            var expected = 13;
            // act
            var actual = _marshaler.SizeOf(data);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SizeOf_on_Complex_type_should_work()
        {
            // arrange
            var data = new ComplexType
            {
                Property1 = 1,
                Property2 = 2,
                Property3 = "123",
                InnerComplexType = new ComplexType
                {
                    Property1 = 1,
                    Property2 = 2,
                    Property3 = "123"
                }
            };

            var expected = 32;
            // act
            var actual = _marshaler.SizeOf(data);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SizeOf_on_DateTime_should_work()
        {
            // arrange
            var data = new DateTime(2021, 2, 1, 12, 0, 0);
            var expected = 8;
            // act
            var actual = _marshaler.SizeOf(data);
            // assert
            Assert.Equal(expected, actual);
        }
    }
}