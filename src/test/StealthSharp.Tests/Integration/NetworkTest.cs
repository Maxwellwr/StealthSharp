#region Copyright

// -----------------------------------------------------------------------
// <copyright file="NetworkTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Tests.DataGenerators;
using Xunit;

namespace StealthSharp.Tests.Integration
{
    public class NetworkTest : IDisposable
    {
        private IStealthSharpClient<ushort, uint, ushort> _client;

        public NetworkTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.Configure<SerializationOptions>(c => c.ArrayCountType = typeof(uint));
            services.AddStealthSharpClient<ushort, uint, ushort, StealthTypeMapper>(c =>
            {
                c.ArrayCountType = typeof(uint);
                c.StringSizeType = typeof(uint);
            });
            services.AddSingleton<IPacketCorrelationGenerator<ushort>, PacketCorrelationGenerator>();
            services.AddSingleton<ICustomConverter<DateTime>, DateTimeConverter>();
            services.AddTransient(typeof(IPacket<,,,>), typeof(TestPacket<,,,>));
            services.AddTransient(typeof(IPacket<,,>), typeof(TestPacket<,,>));
            var sp = services.BuildServiceProvider();

            _client = sp.GetRequiredService<IStealthSharpClient<ushort, uint, ushort>>();
        }

        [Fact]
        public async Task Simple_packet_should_work()
        {
            var port = FindPort();
            _client.Connect(IPAddress.Parse("10.211.55.3"), port);
            _client.Send(new TestPacket<ushort, uint, ushort, (byte, byte, byte, byte, byte)>
                {CorrelationId = 0, TypeId = 5, Body = (3, 2, 2, 0, 1)});

            _client.Send<AboutData>(new TestPacket<ushort, uint, ushort>
                {CorrelationId = 1, TypeId = 12});
            var res = await _client.ReceiveAsync<AboutData>(1);
            Assert.Equal(new ushort[] {8, 11, 4}, res.Body.StealthVersion);
        }

        private int FindPort()
        {
            var tcpClient = new TcpClient("10.211.55.3", 47602);
            var stream = tcpClient.GetStream();
            var buffer = new byte[] {0x04, 0x00, 0xEF, 0xBE, 0xAD, 0xDE};
            stream.Write(buffer, 0, 6);
            stream.Flush();
            buffer = new byte[4];
            var readed = stream.Read(buffer, 0, 2);
            var len = BinaryPrimitives.ReadUInt16LittleEndian(buffer.AsSpan());
            stream.Read(buffer, 0, len);
            if (len == 2)
            {
                return BinaryPrimitives.ReadUInt16LittleEndian(buffer);
            }
            else
            {
                return (int) BinaryPrimitives.ReadUInt32LittleEndian(buffer);
            }
        }

        public void Dispose()
        { 
            _client.Dispose();
        }
    }
}