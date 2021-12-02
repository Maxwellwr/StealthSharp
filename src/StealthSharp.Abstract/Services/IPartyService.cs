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

        Task InviteToPartyAsync(uint id);

        Task PartyAcceptInviteAsync();

        Task PartyCanLootMeAsync(bool value);

        Task PartyDeclineInviteAsync();

        Task PartyLeaveAsync();

        Task PartyPrivateMessageToAsync(uint id, string msg);

        Task PartySayAsync(string msg);

        Task RemoveFromPartyAsync(uint id);
    }
}