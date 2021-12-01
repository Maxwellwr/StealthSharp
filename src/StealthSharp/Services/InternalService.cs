#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="InternalService.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using System.Buffers.Binary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StealthSharp.Enumeration;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    internal class InternalService: BaseService
    {
        private readonly StealthOptions _options;
        private readonly IStealthService _stealthService;
        private readonly Version _supportedVersion = new (9, 2, 0, 0);
        
        public InternalService(IStealthSharpClient client,
            IOptions<StealthOptions>? options,
            IStealthService stealthService) : base(client)
        {
            _options = options?.Value ?? new StealthOptions();
            _stealthService = stealthService;
        }

        internal async Task ConnectToStealthAsync()
        {
            Client.Connect(IPAddress.Parse(_options.Host), FindPort());
            var v = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1,0,0,0);
            (byte LangType, byte Major, byte Minor, byte Build, byte Rev) b = (3, (byte)v.Major, (byte)v.Minor,(byte)v.Build,(byte)v.Revision);
            await Client.SendPacketAsync(PacketType.SCLangVersion, b).ConfigureAwait(false);

            var about = await _stealthService.GetStealthInfoAsync().ConfigureAwait(false);
            var stealthVersion = new Version(about.StealthVersion[0], about.StealthVersion[1], about.StealthVersion[2],
                about.Build);
            if (stealthVersion < _supportedVersion)
                throw new InvalidOperationException(
                    $"Version {stealthVersion} not supported. Minimum supported version is {_supportedVersion}");
        }
        
        private int FindPort()
        {
            var tcpClient = new TcpClient(_options.Host, _options.Port);
            var stream = tcpClient.GetStream();
            var buffer = new byte[] {0x04, 0x00, 0xEF, 0xBE, 0xAD, 0xDE};
            stream.Write(buffer, 0, 6);
            stream.Flush();
            buffer = new byte[4];
            var read = stream.Read(buffer, 0, 2);
            if (read < 2)
                throw new InvalidDataException("Find port: Read data too small");
            var len = BinaryPrimitives.ReadUInt16LittleEndian(buffer.AsSpan());
            stream.Read(buffer, 0, len);
            if (len == 2)
                return BinaryPrimitives.ReadUInt16LittleEndian(buffer);

            return (int) BinaryPrimitives.ReadUInt32LittleEndian(buffer);
        }
    }
}