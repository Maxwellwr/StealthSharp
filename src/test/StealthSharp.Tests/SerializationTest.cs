using System;
using System.Collections.Generic;
using System.Linq;
using StealthSharp.Serialization;
using Xunit;

namespace StealthSharp.Tests
{
    public class SerializationTest
    {
        private readonly IPacketSerializer _serializer;
        public SerializationTest()
        {
            _serializer = new PacketSerializer();
        }
        
        [Theory]
        [InlineData(1, "01000000")]
        [InlineData(1U, "01000000")]
        [InlineData(1L, "01000000")]
        [InlineData((ulong)1L, "01000000")]
        [InlineData((short)1, "0100")]
        [InlineData((ushort)1, "0100")]
        [InlineData(true, "01")]
        [InlineData((byte)1, "01")]
        public void Serialize_simple_type_should_work<T>(T testValue, string expected)
        {
            //arrange
            
            //act
            var result = _serializer.Serialize(testValue);
            
            //assert
            var actual =  ToHexString(result);
            Assert.Equal(expected, actual);
        }

        private static string ToHexString(IEnumerable<byte> array) =>
            string.Concat(array.Select(b => b.ToString("X2")));
    }
}