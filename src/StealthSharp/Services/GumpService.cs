#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GumpService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class GumpService : BaseService, IGumpService
    {
        public GumpService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public async Task<ushort> GetGumpsCountAsync()
        {
            return unchecked((ushort)await Client.SendPacketAsync<uint>(PacketType.SCGetGumpsCount));
        }

        public async Task<bool> GetIsGumpAsync()
        {
            return (await GetGumpsCountAsync()) > 0;
        }

        public void AddGumpIgnoreByID(uint id)
        {
            Client.SendPacket(PacketType.SCAddGumpIgnoreByID, id);
        }

        public void AddGumpIgnoreBySerial(uint serial)
        {
            Client.SendPacket(PacketType.SCAddGumpIgnoreBySerial, serial);
        }

        public void ClearGumpsIgnore()
        {
            Client.SendPacket(PacketType.SCClearGumpsIgnore);
        }

        public void CloseClientGump(uint id)
        {
            Client.SendPacket(PacketType.SCCloseClientGump, id);
        }

        public void CloseSimpleGump(ushort gumpIndex)
        {
            Client.SendPacket(PacketType.SCCloseSimpleGump, gumpIndex);
        }

        public Task<List<string>> GetGumpButtonsDescriptionAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, List<string>>(PacketType.SCGetGumpButtonsDescription, gumpIndex);
        }

        public Task<List<string>> GetGumpFullLinesAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, List<string>>(PacketType.SCGetGumpFullLines, gumpIndex);
        }

        public Task<uint> GetGumpIDAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, uint>(PacketType.SCGetGumpID, gumpIndex);
        }

        public Task<GumpInfo> GetGumpInfoAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, GumpInfo>(PacketType.SCGetGumpInfo, gumpIndex);
        }

        public Task<uint> GetGumpSerialAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, uint>(PacketType.SCGetGumpSerial, gumpIndex);
        }

        public Task<List<string>> GetGumpShortLinesAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, List<string>>(PacketType.SCGetGumpShortLines, gumpIndex);
        }

        public Task<List<string>> GetGumpTextLinesAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, List<string>>(PacketType.SCGetGumpTextLines, gumpIndex);
        }

        public void GumpAutoCheckBox(int checkboxId, int value)
        {
            Client.SendPacket(PacketType.SCGumpAutoCheckBox, (checkboxId, value));
        }

        public void GumpAutoRadiobutton(int radiobuttonId, int value)
        {
            Client.SendPacket(PacketType.SCGumpAutoRadiobutton, (radiobuttonId, value));
        }

        public void GumpAutoTextEntry(int textEntryId, string value)
        {
            Client.SendPacket(PacketType.SCGumpAutoTextEntry, (textEntryId, value));
        }

        public async Task<bool> IsGumpCanBeClosedAsync(ushort gumpIndex)
        {
            return !await Client.SendPacketAsync<ushort, bool>(PacketType.SCGetGumpNoClose, gumpIndex);
        }

        public Task<bool> NumGumpButtonAsync(ushort gumpIndex, int value)
        {
            return Client.SendPacketAsync<(ushort, int), bool>(PacketType.SCNumGumpButton, (gumpIndex, value));
        }

        public Task<bool> NumGumpCheckBoxAsync(ushort gumpIndex, int checkboxId, int value)
        {
            return Client.SendPacketAsync<(ushort, int, int), bool>(PacketType.SCNumGumpCheckBox,
                (gumpIndex, checkboxId, value));
        }

        public Task<bool> NumGumpRadiobuttonAsync(ushort gumpIndex, int radiobuttonId, int value)
        {
            return Client.SendPacketAsync<(ushort, int, int), bool>(PacketType.SCNumGumpRadiobutton,
                (gumpIndex, radiobuttonId, value));
        }

        public Task<bool> NumGumpTextEntryAsync(ushort gumpIndex, int textentryId, string value)
        {
            return Client.SendPacketAsync<(ushort, int, string), bool>(PacketType.SCNumGumpTextEntry,
                (gumpIndex, textentryId, value));
        }

        public void WaitGump(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                WaitGump(BitConverter.ToInt32(Encoding.Unicode.GetBytes(value.Trim()), 0));
            }
        }

        public void WaitGump(int value)
        {
            Client.SendPacket(PacketType.SCWaitGumpInt, value);
        }

        public void WaitTextEntry(string value)
        {
            Client.SendPacket(PacketType.SCWaitGumpTextEntry, value);
        }
    }
}