#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ClientService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class ClientService : BaseService, IClientService
    {
        public ClientService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public Task<bool> ClientHideAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public void ClientPrint(string text)
        {
            Client.SendPacket(PacketType.SCClientPrint, text);
        }

        public void ClientPrintEx(uint senderId, ushort color, ushort font, string text)
        {
            Client.SendPacket(PacketType.SCClientPrintEx, (senderId, color, font, text));
        }

        public void CloseClientUIWindow(UIWindowType uiWindowType, uint id)
        {
            Client.SendPacket(PacketType.SCCloseClientUIWindow, (uiWindowType, id));
        }

        public void UOSay(string text)
        {
            Client.SendPacket(PacketType.SCSendTextToUO, text);
        }

        public void UOSayColor(string text, ushort color)
        {
            Client.SendPacket(PacketType.SCSendTextToUOColor, (text, color));
        }
    }
}