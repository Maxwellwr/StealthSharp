#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SystemService.cs" company="StealthSharp">
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
    public class SystemService : BaseService, ISystemService
    {
        public SystemService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public void SetSilentMode(bool value)
        {
            Client.SendPacket(PacketType.SCSetSilentMode, value);
        }

        public Task<bool> GetSilentModeAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetSilentMode);
        }

        public void Alarm()
        {
            Client.SendPacket(PacketType.SCSetAlarm);
        }

        public void ConsoleEntryReply(string text)
        {
            Client.SendPacket(PacketType.SCConsoleEntryReply, text);
        }

        public void ConsoleEntryUnicodeReply(string text)
        {
            Client.SendPacket(PacketType.SCConsoleEntryUnicodeReply, text);
        }

        public void HelpRequest()
        {
            Client.SendPacket(PacketType.SCHelpRequest);
        }

        public void QuestRequest()
        {
            Client.SendPacket(PacketType.SCQuestRequest);
        }

        public void RequestStats(uint objId)
        {
            Client.SendPacket(PacketType.SCRequestStats, objId);
        }

        public Task<bool> ClientHide(uint id)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCClientHide, id);
        }
    }
}