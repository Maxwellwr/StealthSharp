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

        public Task SetSilentModeAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetSilentMode, value);
        }

        public Task<bool> GetSilentModeAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetSilentMode);
        }

        public Task AlarmAsync()
        {
            return Client.SendPacketAsync(PacketType.SCSetAlarm);
        }

        public Task ConsoleEntryReplyAsync(string text)
        {
            return Client.SendPacketAsync(PacketType.SCConsoleEntryReply, text);
        }

        public Task ConsoleEntryUnicodeReplyAsync(string text)
        {
            return Client.SendPacketAsync(PacketType.SCConsoleEntryUnicodeReply, text);
        }

        public Task HelpRequestAsync()
        {
            return Client.SendPacketAsync(PacketType.SCHelpRequest);
        }

        public Task QuestRequestAsync()
        {
            return Client.SendPacketAsync(PacketType.SCQuestRequest);
        }

        public Task RequestStatsAsync(uint objId)
        {
            return Client.SendPacketAsync(PacketType.SCRequestStats, objId);
        }

        public Task<bool> ClientHide(uint id)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCClientHide, id);
        }
    }
}