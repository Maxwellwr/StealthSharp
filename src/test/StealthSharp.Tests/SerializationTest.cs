using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Serialization;
using StealthSharp.Tests.DataGenerators;
using Xunit;

namespace StealthSharp.Tests
{
    public class SerializationTest
    {
        private readonly IPacketSerializer _serializer;
        public SerializationTest()
        {
            _serializer = new PacketSerializer(new ReflectionCache(), new BitConvert(new Mock<IOptions<SerializationOptions>>().Object));
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
            var result = _serializer.Serialize(testValue);
            
            //assert
            var actual =  ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Serialize_complex_type_should_work<T>(Packet<T> testValue, string expected)
        {
            //arrange
            
            //act
            var result = _serializer.Serialize(testValue);
            
            //assert
            var actual =  ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_Tuple_type_should_work()
        {
            //arrange
            var testValue = new Packet<(int, short, byte, byte[])>()
            {
                Method = 123,
                ReturnId = 255,
                Body = (1, 2, 3, new byte[] {4, 5, 6, 7})
            };
            string expected = "170000007B00FF00010000000200030400000004050607";
            //act
            var result = _serializer.Serialize(testValue);
            
            //assert
            var actual =  ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        private static string ToHexString(IEnumerable<byte> array) =>
            string.Concat(array.Select(b => b.ToString("X2")));
    }
}