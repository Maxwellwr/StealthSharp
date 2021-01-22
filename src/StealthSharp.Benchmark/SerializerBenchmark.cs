using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Serialization;

namespace StealthSharp.Benchmark
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [MemoryDiagnoser]
    public class SerializerBenchmark
    {
        private readonly IPacketSerializer _serializer;
        private readonly Packet<AboutData> _testData;

        public SerializerBenchmark()
        {
            _serializer = new PacketSerializer(new ReflectionCache(),
                new BitConvert(new Mock<IOptions<SerializationOptions>>().Object));
            _testData = new Packet<AboutData>()
            {
                Method = 25,
                ReturnId = 1,
                Body = new AboutData()
                {
                    Property1 = 1,
                    Property2 = 2,
                    Property3 = 3
                }
            };
        }

        [Benchmark]
        public byte[] Serialize()
        {
            using var res = _serializer.Serialize(_testData);
            return res.Memory.ToArray();
        }


        [Benchmark]
        public Packet<AboutData> Deserialize()
        {
            using var res = _serializer.Serialize(_testData);
            var deres = _serializer.Deserialize<Packet<AboutData>>(res);
            return deres;
        }
    }
}