#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PartyService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class PartyService : BaseService, IPartyService
    {
        public PartyService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<bool> GetInPartyAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCInParty);
        }

        public Task<uint[]> GetPartyMembersListAsync()
        {
            return Client.SendPacketAsync<uint[]>(PacketType.SCPartyMembersList);
        }

        public Task InviteToPartyAsync(uint id)
        {
            return Client.SendPacketAsync(PacketType.SCInviteToParty, id);
        }

        public Task PartyAcceptInviteAsync()
        {
            return Client.SendPacketAsync(PacketType.SCPartyAcceptInvite);
        }

        public Task PartyCanLootMeAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCPartyCanLootMe, value);
        }

        public Task PartyDeclineInviteAsync()
        {
            return Client.SendPacketAsync(PacketType.SCPartyDeclineInvite);
        }

        public Task PartyLeaveAsync()
        {
            return Client.SendPacketAsync(PacketType.SCPartyLeave);
        }

        public Task PartyPrivateMessageToAsync(uint id, string msg)
        {
            return Client.SendPacketAsync(PacketType.SCPartyMessageTo, (id, msg));
        }

        public Task PartySayAsync(string msg)
        {
            return Client.SendPacketAsync(PacketType.SCPartySay, msg);
        }

        public Task RemoveFromPartyAsync(uint id)
        {
            return Client.SendPacketAsync(PacketType.SCRemoveFromParty, id);
        }
    }
}