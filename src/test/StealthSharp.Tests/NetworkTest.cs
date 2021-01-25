using System;
using System.Buffers.Binary;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Tests.DataGenerators;
using Xunit;

namespace StealthSharp.Tests
{
    public class NetworkTest: IAsyncDisposable
    {
        private IStealthSharpClient<ushort, uint, ushort> _client;
        public NetworkTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.Configure<SerializationOptions>(c => c.ArrayCountType = typeof(uint));
            services.AddStealthSharpClient<ushort, uint, ushort, SimpleTypeMapper>();
            services.AddTransient(typeof(IPacket<,,,>), typeof(Packet<,,,>));
            services.AddTransient(typeof(IPacket<,,>), typeof(Packet<,,>));
            var sp = services.BuildServiceProvider();

            _client = sp.GetRequiredService<IStealthSharpClient<ushort, uint, ushort>>();
        }

        [Fact]
        public async Task Simple_packet_should_work()
        {
            var port = FindPort();
            _client.Connect( IPAddress.Parse("10.211.55.3"), port);
            await  _client.SendAsync(new Packet<ushort, uint, ushort, (byte, byte,byte,byte,byte)>
                {CorrelationId = 0, TypeId = 5, Body = (3, 2, 2, 0, 1)});
            
            await _client.SendAsync(new Packet<ushort, uint, ushort>
                {CorrelationId = 1, TypeId = 12});
            var res = await _client.ReceiveAsync<AboutData>(1);
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
                    return (int)BinaryPrimitives.ReadUInt32LittleEndian(buffer);
                }
        }

        public ValueTask DisposeAsync()
        {
            return _client.DisposeAsync();
        }
    }

    public class SimpleTypeMapper : ITypeMapper<ushort>
    {
        public Type? GetMappedType(ushort typeIdentify, ushort requestTypeIdentify)
        {
            switch (typeIdentify)
            {
                case 1:
                    switch (requestTypeIdentify)
                    {
                        case 12:
                            return typeof(AboutData);
                    }
                    break;
            }

            return null;
        }
    }
}