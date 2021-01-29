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
using StealthSharp.Serialization;

namespace StealthSharp.Benchmark
{
    [MarkdownExporter, HtmlExporter]
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [MemoryDiagnoser]
    public class SerializerBenchmark
    {
        private readonly IPacketSerializer _serializer;
        private readonly Packet<AboutData> _testData;

        public SerializerBenchmark()
        {
            var refCache = new ReflectionCache();
            var spMock = new Mock<IServiceProvider>();
            var bitConverter =new BitConvert(refCache);
            _serializer = new PacketSerializer(new Mock<IOptions<SerializationOptions>>().Object,refCache, new CustomConverterFactory(spMock.Object), bitConverter);
            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_serializer));

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