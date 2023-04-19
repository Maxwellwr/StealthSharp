#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ConnectionService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class ConnectionService : BaseService, IConnectionService
    {
        public ConnectionService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task SetARStatusAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetARStatus, value);
        }

        public Task<bool> GetARStatusAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetARStatus);
        }

        public Task<bool> GetConnectedAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetConnectedStatus);
        }

        public Task<DateTime> GetConnectedTimeAsync()
        {
            return Client.SendPacketAsync<DateTime>(PacketType.SCGetConnectedTime);
        }

        public Task<DateTime> GetDisconnectedTimeAsync()
        {
            return Client.SendPacketAsync<DateTime>(PacketType.SCGetDisconnectedTime);
        }

        public Task SetPauseScriptOnDisconnectStatusAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetPauseScriptOnDisconnectStatus, value);
        }

        public Task<bool> GetPauseScriptOnDisconnectStatusAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetPauseScriptOnDisconnectStatus);
        }

        public Task<string> GetGameServerIPStringAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGameServerIPString);
        }

        public Task<string> GetProxyIPAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetProxyIP);
        }

        public Task<ushort> GetProxyPortAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetProxyPort);
        }

        public Task<bool> GetUseProxyAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetUseProxy);
        }

        public Task<int> ChangeProfileAsync(string name)
        {
            return Client.SendPacketAsync<string, int>(PacketType.SCChangeProfile, name);
        }

        public Task<int> ChangeProfileAsync(string name, string shardName, string charName)
        {
            return Client.SendPacketAsync<(string, string, string), int>(PacketType.SCChangeProfileEx, (name, shardName, charName));
        }

        public async Task<bool> CheckLagAsync(int timeoutMs)
        {
            var result = false;
            await CheckLagBeginAsync().ConfigureAwait(false);
            var stopTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, timeoutMs);
            var checkLagEndRes = false;
            do
            {
                Thread.Sleep(20);
                checkLagEndRes = await Client.SendPacketAsync<bool>(PacketType.SCIsCheckLagEnd).ConfigureAwait(false);
            } while (DateTime.Now <= stopTime && !checkLagEndRes);

            if (checkLagEndRes) result = true;

            await CheckLagEndAsync().ConfigureAwait(false);

            return result;
        }

        public Task CheckLagBeginAsync()
        {
            return Client.SendPacketAsync(PacketType.SCCheckLagBegin);
        }

        public Task CheckLagEndAsync()
        {
            return Client.SendPacketAsync(PacketType.SCCheckLagEnd);
        }

        public Task ConnectAsync()
        {
            return Client.SendPacketAsync(PacketType.SCConnect);
        }

        public Task DisconnectAsync()
        {
            return Client.SendPacketAsync(PacketType.SCDisconnect);
        }

        public Task SetARExtParamsAsync(string shardName, string charName, bool useAtEveryConnect)
        {
            return Client.SendPacketAsync(PacketType.SCSetARExtParams, (shardName, charName, useAtEveryConnect));
        }
    }
}