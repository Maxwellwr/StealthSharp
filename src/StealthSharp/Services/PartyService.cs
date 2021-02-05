#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PartyService.cs" company="StealthSharp">
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
    public class PartyService : BaseService, IPartyService
    {
        public PartyService(IStealthSharpClient<ushort, uint, ushort> client)
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

        public void InviteToParty(uint id)
        {
            Client.SendPacket(PacketType.SCInviteToParty, id);
        }

        public void PartyAcceptInvite()
        {
            Client.SendPacket(PacketType.SCPartyAcceptInvite);
        }

        public void PartyCanLootMe(bool value)
        {
            Client.SendPacket(PacketType.SCPartyCanLootMe, value);
        }

        public void PartyDeclineInvite()
        {
            Client.SendPacket(PacketType.SCPartyDeclineInvite);
        }

        public void PartyLeave()
        {
            Client.SendPacket(PacketType.SCPartyLeave);
        }

        public void PartyPrivateMessageTo(uint id, string msg)
        {
            Client.SendPacket(PacketType.SCPartyMessageTo, (id, msg));
        }

        public void PartySay(string msg)
        {
            Client.SendPacket(PacketType.SCPartySay, msg);
        }

        public void RemoveFromParty(uint id)
        {
            Client.SendPacket(PacketType.SCRemoveFromParty, id);
        }
    }
}