#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ICQService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class ICQService : BaseService, IICQService
    {
        public ICQService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public Task<bool> GetICQConnectedAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCICQ_GetConnectedStatus);
        }

        public Task ICQConnectAsync(string uin, string password)
        {
             return Client.SendPacketAsync(PacketType.SCICQ_Connect, (uin, password));
        }

        public Task ICQDisconnectAsync()
        {
             return Client.SendPacketAsync(PacketType.SCICQ_Disconnect);
        }

        public Task ICQSendTextAsync(string toUin, string text)
        {
             return Client.SendPacketAsync(PacketType.SCICQ_SendText, (toUin, text));
        }

        public Task ICQSetStatusAsync(byte num)
        {
             return Client.SendPacketAsync(PacketType.SCICQ_SetStatus, num);
        }

        public Task ICQSetXStatusAsync(byte num)
        {
             return Client.SendPacketAsync(PacketType.SCICQ_SetXStatus, num);
        }
    }
}