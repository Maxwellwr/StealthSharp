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
using StealthSharp.Enum;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Tests.DataGenerators;
using Xunit;

namespace StealthSharp.Tests.Integration
{
    public class NetworkTest : IDisposable
    {
        private IStealthSharpClient _client;

        public NetworkTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.Configure<SerializationOptions>(c => c.ArrayCountType = typeof(uint));
            services.AddStealthSharpClient(c =>
            {
                c.ArrayCountType = typeof(uint);
                c.StringSizeType = typeof(uint);
            });
            services.AddSingleton<IPacketCorrelationGenerator<ushort>, PacketCorrelationGenerator>();
            services.AddSingleton<ICustomConverter<DateTime>, DateTimeConverter>();
            var sp = services.BuildServiceProvider();

            _client = sp.GetRequiredService<IStealthSharpClient>();
        }

        [Fact]
        public async Task Simple_packet_should_work()
        {
            var port = FindPort();
            _client.Connect(IPAddress.Parse("10.211.55.3"), port);
            await _client.SendAsync(PacketType.SCLangVersion, ((byte)3, (byte)2, (byte)2, (byte)0, (byte)1));

            var(result, correlationId) = await _client.SendAsync(
                PacketType.SCGetStealthInfo);
            Assert.True(result);
            var res = await _client.ReceiveAsync<AboutData>(correlationId);
            Assert.Equal(new ushort[] {8, 11, 4}, res.StealthVersion);
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