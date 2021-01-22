using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Serialization;
using StealthSharp.Tests.DataGenerators;
using Xunit;

namespace StealthSharp.Tests
{
    public class DeserializationTest
    {
        private readonly IPacketSerializer _serializer;

        public DeserializationTest()
        {
            _serializer = new PacketSerializer(new ReflectionCache(), new BitConvert(new Mock<IOptions<SerializationOptions>>().Object));
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
            var result = new SerializationResult( bytes.Length) ;
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<T>(result);

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SerializeDataGenerator))]
        public void Deserialize_complex_type_should_work<T>(Packet<T> expected, string testValue)
        {
            //arrange
            var bytes = FromHexString(testValue);
            var result = new SerializationResult( bytes.Length) ;
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<Packet<T>>(result);

            //assert
            Assert.Equal(expected, actual);
            Assert.Equal(expected.Body, actual.Body);
        }

        [Fact]
        public void Deserialize_Tuple_type_should_work()
        {
            //arrange
            var expected = new Packet<(int, short, byte, byte[])>()
            {
                Length = 23,
                Method = 123,
                ReturnId = 255,
                Body = (1, 2, 3, new byte[] {4, 5, 6, 7})
            };
            string testValue = "170000007B00FF00010000000200030400000004050607";
            var bytes = FromHexString(testValue);
            var result = new SerializationResult( MemoryMarshal.Read<int>( bytes)) ;
            bytes.AsSpan().CopyTo(result.Memory.Span);
            //act
            var actual = _serializer.Deserialize<Packet<(int, short, byte, byte[])>>(result);

            //assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.Method, actual.Method);
            Assert.Equal(expected.ReturnId, actual.ReturnId);
            Assert.Equal(expected.Body.Item1, actual.Body.Item1);
            Assert.Equal(expected.Body.Item2, actual.Body.Item2);
            Assert.Equal(expected.Body.Item3, actual.Body.Item3);
            Assert.Equal(expected.Body.Item4, actual.Body.Item4);
        }

        private static byte[] FromHexString(string hex) =>
            Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
    }
}