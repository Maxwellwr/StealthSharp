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
        public void Serialize_complex_type_should_work<T>(Packet<ushort, uint, ushort, T> testValue, string expected)
        {
            //arrange
            
            //act
            var result = _serializer.Serialize(testValue);
            
            //assert
            var actual =  ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialize_empty_body_should_work()
        {
            //arrange
            var testValue = new Packet<ushort, uint, ushort>()
            {
                Length = 8,
                TypeId = 12,
                CorrelationId = 1
            };
            var expected = "040000000C000100";
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
            var testValue = new Packet<ushort, uint, ushort, (int, short, byte, byte[])>()
            {
                TypeId = 123,
                CorrelationId = 255,
                Body = (1, 2, 3, new byte[] {4, 5, 6, 7})
            };
            string expected = "130000007B00FF00010000000200030400000004050607";
            //act
            var result = _serializer.Serialize(testValue);
            
            //assert
            var actual =  ToHexString(result.Memory.ToArray());
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
            var actual =  ToHexString(result.Memory.ToArray());
            Assert.Equal(expected, actual);
        }

        private static string ToHexString(IEnumerable<byte> array) =>
            string.Concat(array.Select(b => b.ToString("X2")));
    }
}