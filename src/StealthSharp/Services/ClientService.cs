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
        public ClientService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<bool> ClientHideAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public Task ClientPrintAsync(string text)
        {
            return Client.SendPacketAsync(PacketType.SCClientPrint, text);
        }

        public Task ClientPrintExAsync(uint senderId, ushort color, ushort font, string text)
        {
            return Client.SendPacketAsync(PacketType.SCClientPrintEx, (senderId, color, font, text));
        }

        public Task CloseClientUIWindowAsync(UIWindowType uiWindowType, uint id)
        {
            return Client.SendPacketAsync(PacketType.SCCloseClientUIWindow, (uiWindowType, id));
        }

        public Task UOSayAsync(string text)
        {
            return Client.SendPacketAsync(PacketType.SCSendTextToUO, text);
        }

        public Task UOSayColorAsync(string text, ushort color)
        {
            return Client.SendPacketAsync(PacketType.SCSendTextToUOColor, (text, color));
        }
    }
}