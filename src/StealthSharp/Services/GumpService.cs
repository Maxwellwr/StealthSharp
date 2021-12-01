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
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class GumpService : BaseService, IGumpService
    {
        public GumpService(IStealthSharpClient client)
            : base(client)
        {
        }

        public async Task<ushort> GetGumpsCountAsync()
        {
            return await Client.SendPacketAsync<ushort>(PacketType.SCGetGumpsCount).ConfigureAwait(false);
        }

        public async Task<bool> GetIsGumpAsync()
        {
            return (await GetGumpsCountAsync().ConfigureAwait(false)) > 0;
        }

        public Task AddGumpIgnoreByIdAsync(uint id)
        {
            return Client.SendPacketAsync(PacketType.SCAddGumpIgnoreByID, id);
        }

        public Task AddGumpIgnoreBySerialAsync(uint serial)
        {
            return Client.SendPacketAsync(PacketType.SCAddGumpIgnoreBySerial, serial);
        }

        public Task ClearGumpsIgnoreAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClearGumpsIgnore);
        }

        public Task CloseClientGumpAsync(uint id)
        {
            return Client.SendPacketAsync(PacketType.SCCloseClientGump, id);
        }

        public Task CloseSimpleGumpAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync(PacketType.SCCloseSimpleGump, gumpIndex);
        }

        public Task<List<string>> GetGumpButtonsDescriptionAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, List<string>>(PacketType.SCGetGumpButtonsDescription, gumpIndex);
        }

        public Task<List<string>> GetGumpFullLinesAsync(ushort gumpIndex)
        {
            return Client.SendPacketAsync<ushort, List<string>>(PacketType.SCGetGumpFullLines, gumpIndex);
        }

        public Task<uint> GetGumpIdAsync(ushort gumpIndex)
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

        public Task GumpAutoCheckBoxAsync(int checkboxId, int value)
        {
            return Client.SendPacketAsync(PacketType.SCGumpAutoCheckBox, (checkboxId, value));
        }

        public Task GumpAutoRadiobuttonAsync(int radiobuttonId, int value)
        {
            return Client.SendPacketAsync(PacketType.SCGumpAutoRadiobutton, (radiobuttonId, value));
        }

        public Task GumpAutoTextEntryAsync(int textEntryId, string value)
        {
            return Client.SendPacketAsync(PacketType.SCGumpAutoTextEntry, (textEntryId, value));
        }

        public async Task<bool> IsGumpCanBeClosedAsync(ushort gumpIndex)
        {
            return !await Client.SendPacketAsync<ushort, bool>(PacketType.SCGetGumpNoClose, gumpIndex).ConfigureAwait(false);
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

        public async Task WaitGumpAsync(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await WaitGumpAsync(BitConverter.ToInt32(Encoding.Unicode.GetBytes(value.Trim()), 0)).ConfigureAwait(false);
            }
        }

        public Task WaitGumpAsync(int value)
        {
            return Client.SendPacketAsync(PacketType.SCWaitGumpInt, value);
        }

        public Task WaitTextEntryAsync(string value)
        {
            return Client.SendPacketAsync(PacketType.SCWaitGumpTextEntry, value);
        }
    }
}