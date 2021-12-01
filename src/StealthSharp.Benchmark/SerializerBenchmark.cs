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
using StealthSharp.Enumeration;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Serialization.Converters;

namespace StealthSharp.Benchmark
{
    [MarkdownExporter, HtmlExporter]
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [MemoryDiagnoser]
    public class SerializerBenchmark
    {
        private readonly IMarshaler _marshaler;
        private readonly PacketHeader _testData;

        public SerializerBenchmark()
        {
            var refCache = new ReflectionCache();
            var spMock = new Mock<IServiceProvider>();
            var converter = new CustomConverterFactory(spMock.Object);
            _marshaler = new Marshaler(new Mock<IOptions<SerializationOptions>>().Object,
                refCache, converter);

            spMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(ICustomConverter<DateTime>))))
                .Returns(new DateTimeConverter(_marshaler));

            _testData = new PacketHeader()
            {
                PacketType = PacketType.SCGetStealthInfo,
                Length = 10
            };
        }

        [Benchmark]
        public byte[] Serialize()
        {
            using var res = _marshaler.Serialize(_testData);
            return res.Memory.ToArray();
        }


        [Benchmark]
        public AboutData Deserialize()
        {
            using var res = new SerializationResult(6);
            new byte[]{1,0,2,0,3,0}.AsSpan().CopyTo(res.Memory.Span);
            var deres = _marshaler.Deserialize<AboutData>(res);
            return deres;
        }
    }
}