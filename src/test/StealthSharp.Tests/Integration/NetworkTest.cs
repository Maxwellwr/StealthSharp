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
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Serialization.Converters;
using Xunit;

namespace StealthSharp.Tests.Integration
{
    [Trait( "Category", "Integration")]
    public class NetworkTest : IDisposable
    {
        private const string STEALTH_API_HOST = "127.0.0.1";
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
            _client.Connect(IPAddress.Parse(STEALTH_API_HOST), port);
            await _client.SendAsync(PacketType.SCLangVersion, ((byte)3, (byte)2, (byte)2, (byte)0, (byte)1));

            var(result, correlationId) = await _client.SendAsync(
                PacketType.SCGetStealthInfo);
            Assert.True(result);
            var res = await _client.ReceiveAsync<AboutData>(correlationId);
            Assert.Equal(new ushort[] {9, 2, 0}, res.StealthVersion);
        }

        private int FindPort()
        {
            var tcpClient = new TcpClient(STEALTH_API_HOST, 47602);
            var stream = tcpClient.GetStream();
            var buffer = new byte[] {0x04, 0x00, 0xEF, 0xBE, 0xAD, 0xDE};
            stream.Write(buffer, 0, 6);
            stream.Flush();
            buffer = new byte[4];
            var read = 0;
            while(read != 2)
                read += stream.Read(buffer, read, 2);
            var len = BinaryPrimitives.ReadUInt16LittleEndian(buffer.AsSpan());
            stream.Read(buffer, 0, len);
            if (len == 2)
            {
                return BinaryPrimitives.ReadUInt16LittleEndian(buffer);
            }

            return (int) BinaryPrimitives.ReadUInt32LittleEndian(buffer);
        }

        public void Dispose()
        { 
            _client.Dispose();
        }
    }
}