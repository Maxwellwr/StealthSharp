#region Copyright

// -----------------------------------------------------------------------
// <copyright file="WireFormatTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.MockServer;
using StealthSharp.Model;
using StealthSharp.Serialization;
using Xunit;

#endregion

namespace StealthSharp.Tests.Unit
{
    /// <summary>
    ///     Pins the exact bytes produced by <see cref="Marshaler" /> with the options the client uses (uint length
    ///     prefixes). The expected hex was captured from the original reflection based implementation, so any
    ///     marshaler rewrite has to keep the wire format byte for byte.
    /// </summary>
    [Trait("Category", "Unit")]
    public class WireFormatTest
    {
        private readonly IMarshaler _marshaler;

        public WireFormatTest()
        {
            var services = new ServiceCollection();
            services.AddStealthSharpSerialization(o =>
            {
                o.ArrayCountType = typeof(uint);
                o.StringSizeType = typeof(uint);
            });
            _marshaler = services.BuildServiceProvider().GetRequiredService<IMarshaler>();
        }

        private const string AboutHex =
            "03000000090006000100010000000000801DE6400100080000006D006F0063006B00";

        [Fact]
        public void About_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(new AboutData { StealthVersion = new ushort[] { 9, 6, 1 }, Build = 1, BuildDate = new DateTime(2024, 1, 1), GitRevNumber = 1, GitRevision = "mock" });

            Assert.Equal(AboutHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void About_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(AboutHex);
            var value = _marshaler.Deserialize(input, typeof(StealthSharp.Model.AboutData));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(AboutHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string GumpHex =
            "010000000200000064006400010000000000000000000000000000000300000000000000000000000000000000000000"
            + "000000000000000000000000000000000000000001000000010000000100000001000000000000000000000001000000"
            + "000000000100000002000000020000000200000002000000000000000000000002000000000000000200000000000000"
            + "000000000000000000000000000000000000000000000000000000000300000000000000000000000000000000000000"
            + "000000000000000001000000010000000000000001000000000000000100000002000000020000000000000002000000"
            + "000000000200000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
            + "00000000";

        [Fact]
        public void Gump_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(MockDefaults.CreateGump(3));

            Assert.Equal(GumpHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void Gump_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(GumpHex);
            var value = _marshaler.Deserialize(input, typeof(GumpInfo));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(GumpHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string IdsHex =
            "04000000010000000200000003000000FFFFFFFF";

        [Fact]
        public void Ids_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(new uint[] { 1, 2, 3, 0xFFFFFFFF });

            Assert.Equal(IdsHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void Ids_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(IdsHex);
            var value = _marshaler.Deserialize(input, typeof(uint[]));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(IdsHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string ListHex =
            "02000000FFFFFFFF05000000";

        [Fact]
        public void List_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(new List<int> { -1, 5 });

            Assert.Equal(ListHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void List_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(ListHex);
            var value = _marshaler.Deserialize(input, typeof(List<int>));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(ListHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string TextHex =
            "0E000000480065006C006C006F0020001604";

        [Fact]
        public void Text_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>("Hello \u0416");

            Assert.Equal(TextHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void Text_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(TextHex);
            var value = _marshaler.Deserialize(input, typeof(string));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(TextHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string TupleHex =
            "010200030000000400000061006200";

        [Fact]
        public void Tuple_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(((byte)1, (ushort)2, 3u, "ab"));

            Assert.Equal(TupleHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void Tuple_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(TupleHex);
            var value = _marshaler.Deserialize(input, typeof((byte, ushort, uint, string)));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(TupleHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string EventHex =
            "02030016000000530069006D0070006C00650020007400650078007400000C000000530065006E0064006500720001E7"
            + "000000";

        [Fact]
        public void Event_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(new ServerEventData<SpeechEvent>(EventType.Speech, new SpeechEvent { Text = "Simple text", SenderName = "Sender", Sender = new Identity { Id = 231 } }));

            Assert.Equal(EventHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void Event_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(EventHex);
            var value = _marshaler.Deserialize(input, typeof(ServerEventData));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(EventHex, Convert.ToHexString(result.Memory.Span));
        }

        private const string EnumHex =
            "E500";

        [Fact]
        public void Enum_should_serialize_to_golden_bytes()
        {
            using var result = _marshaler.Serialize<object>(PacketType.SCGetGumpInfo);

            Assert.Equal(EnumHex, Convert.ToHexString(result.Memory.Span));
        }

        [Fact]
        public void Enum_should_deserialize_and_serialize_back_to_golden_bytes()
        {
            using var input = ToResult(EnumHex);
            var value = _marshaler.Deserialize(input, typeof(PacketType));
            using var result = _marshaler.Serialize(value);

            Assert.Equal(EnumHex, Convert.ToHexString(result.Memory.Span));
        }

        private static SerializationResult ToResult(string hex)
        {
            var bytes = Convert.FromHexString(hex);
            var result = new SerializationResult(bytes.Length);
            bytes.CopyTo(result.Memory.Span);
            return result;
        }
    }
}
