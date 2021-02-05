#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IPartyService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IPartyService
    {
        Task<bool> GetInPartyAsync();

        Task<uint[]> GetPartyMembersListAsync();

        void InviteToParty(uint iD);

        void PartyAcceptInvite();

        void PartyCanLootMe(bool value);

        void PartyDeclineInvite();

        void PartyLeave();

        void PartyPrivateMessageTo(uint iD, string msg);

        void PartySay(string msg);

        void RemoveFromParty(uint iD);
    }
}