#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MarshalerBenchmark.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.MockServer;
using StealthSharp.Model;
using StealthSharp.Network;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Benchmark
{
    /// <summary>
    ///     <see cref="Marshaler" /> on realistic payloads and with the same options the client uses (uint length
    ///     prefixes). Run: <c>-- --filter '*MarshalerBenchmark*' --inProcess</c>.
    /// </summary>
    [MemoryDiagnoser]
    public class MarshalerBenchmark
    {
        private readonly IMarshaler _marshaler;

        private readonly PacketHeader _header = new() { PacketType = PacketType.SCGetStealthInfo, Length = 10 };

        private readonly StealthSharp.Model.AboutData _about = new()
        {
            StealthVersion = new ushort[] { 9, 6, 1 }, Build = 1, BuildDate = new DateTime(2024, 1, 1),
            GitRevNumber = 1, GitRevision = "mock"
        };

        private readonly GumpInfo _gumpSmall = MockDefaults.CreateGump(5);
        private readonly GumpInfo _gumpLarge = MockDefaults.CreateGump(200);
        private readonly uint[] _ids = Enumerable.Range(0, 1000).Select(i => (uint)i).ToArray();
        private readonly string _text = new('x', 200);

        private readonly ServerEventData _speech = new ServerEventData<SpeechEvent>(EventType.Speech,
            new SpeechEvent { Text = "Simple text", SenderName = "Sender", Sender = new Identity { Id = 231 } });

        private ISerializationResult _headerBytes = null!;
        private ISerializationResult _aboutBytes = null!;
        private ISerializationResult _gumpSmallBytes = null!;
        private ISerializationResult _gumpLargeBytes = null!;
        private ISerializationResult _idsBytes = null!;
        private ISerializationResult _textBytes = null!;
        private ISerializationResult _speechBytes = null!;

        public MarshalerBenchmark()
        {
            var services = new ServiceCollection();
            services.AddStealthSharpSerialization(o =>
            {
                o.ArrayCountType = typeof(uint);
                o.StringSizeType = typeof(uint);
            });
            _marshaler = services.BuildServiceProvider().GetRequiredService<IMarshaler>();
        }

        [GlobalSetup]
        public void Setup()
        {
            // Serialized once; the results are reused (and never returned to the pool) by the Deserialize benchmarks.
            _headerBytes = _marshaler.Serialize(_header);
            _aboutBytes = _marshaler.Serialize(_about);
            _gumpSmallBytes = _marshaler.Serialize(_gumpSmall);
            _gumpLargeBytes = _marshaler.Serialize(_gumpLarge);
            _idsBytes = _marshaler.Serialize(_ids);
            _textBytes = _marshaler.Serialize(_text);
            _speechBytes = _marshaler.Serialize(_speech);
        }

        private int Serialize<T>(T value) where T : notnull
        {
            using var result = _marshaler.Serialize(value);
            return result.Length;
        }

        [Benchmark]
        public int Serialize_Header() => Serialize(_header);

        [Benchmark]
        public int Serialize_About() => Serialize(_about);

        [Benchmark]
        public int Serialize_Text200() => Serialize(_text);

        [Benchmark]
        public int Serialize_Ids1000() => Serialize(_ids);

        [Benchmark]
        public int Serialize_Event() => Serialize(_speech);

        [Benchmark]
        public int Serialize_GumpSmall() => Serialize(_gumpSmall);

        [Benchmark]
        public int Serialize_GumpLarge() => Serialize(_gumpLarge);

        [Benchmark]
        public PacketHeader Deserialize_Header() => _marshaler.Deserialize<PacketHeader>(_headerBytes);

        [Benchmark]
        public StealthSharp.Model.AboutData Deserialize_About() =>
            _marshaler.Deserialize<StealthSharp.Model.AboutData>(_aboutBytes);

        [Benchmark]
        public string Deserialize_Text200() => _marshaler.Deserialize<string>(_textBytes);

        [Benchmark]
        public uint[] Deserialize_Ids1000() => _marshaler.Deserialize<uint[]>(_idsBytes);

        [Benchmark]
        public ServerEventData Deserialize_Event() => _marshaler.Deserialize<ServerEventData>(_speechBytes);

        [Benchmark]
        public GumpInfo Deserialize_GumpSmall() => _marshaler.Deserialize<GumpInfo>(_gumpSmallBytes);

        [Benchmark]
        public GumpInfo Deserialize_GumpLarge() => _marshaler.Deserialize<GumpInfo>(_gumpLargeBytes);
    }
}
