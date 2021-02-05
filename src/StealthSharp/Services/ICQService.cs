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

        public void ICQConnect(string uin, string password)
        {
             Client.SendPacket(PacketType.SCICQ_Connect, (uin, password));
        }

        public void ICQDisconnect()
        {
             Client.SendPacket(PacketType.SCICQ_Disconnect);
        }

        public void ICQSendText(string toUin, string text)
        {
             Client.SendPacket(PacketType.SCICQ_SendText, (toUin, text));
        }

        public void ICQSetStatus(byte num)
        {
             Client.SendPacket(PacketType.SCICQ_SetStatus, num);
        }

        public void ICQSetXStatus(byte num)
        {
             Client.SendPacket(PacketType.SCICQ_SetXStatus, num);
        }
    }
}