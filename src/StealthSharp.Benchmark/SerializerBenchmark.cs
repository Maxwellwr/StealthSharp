#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializerBenchmark.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Enum;
using StealthSharp.Network;
using StealthSharp.Serialization;

namespace StealthSharp.Benchmark
{
    [MarkdownExporter, HtmlExporter]
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [MemoryDiagnoser]
    public class SerializerBenchmark
    {
        private readonly IPacketSerializer _serializer;
        private readonly PacketHeader _testData;

        public SerializerBenchmark()
        {
            var refCache = new ReflectionCache();
            var spMock = new Mock<IServiceProvider>();
            var converter = new CustomConverterFactory(spMock.Object);
            var marshaler = new Marshaler(refCache, null, converter);
            _serializer = new PacketSerializer(new Mock<IOptions<SerializationOptions>>().Object,
                refCache, marshaler, converter);

            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_serializer, marshaler));

            _testData = new PacketHeader()
            {
                PacketType = PacketType.SCGetStealthInfo,
                CorrelationId = 1,
                Length = 10
            };
        }

        [Benchmark]
        public byte[] Serialize()
        {
            using var res = _serializer.Serialize(_testData);
            return res.Memory.ToArray();
        }


        [Benchmark]
        public AboutData Deserialize()
        {
            using var res = new SerializationResult(6);
            new byte[]{1,0,2,0,3,0}.AsSpan().CopyTo(res.Memory.Span);
            var deres = _serializer.Deserialize<AboutData>(res);
            return deres;
        }
    }
}