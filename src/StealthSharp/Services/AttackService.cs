﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="AttackService.cs" company="StealthSharp">
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
    public class AttackService : BaseService, IAttackService
    {
        private readonly ICharStatsService _charStatsService;
        private readonly IGameObjectService _gameObjectService;


        public AttackService(IStealthSharpClient client,
            ICharStatsService charStatsService,
            IGameObjectService gameObjectService)
            : base(client)
        {
            _charStatsService = charStatsService;
            _gameObjectService = gameObjectService;
        }

        public Task<uint> GetLastAttackAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastAttack);
        }

        public Task SetWarModeAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetWarMode, value);
        }

        public async Task<bool> GetWarModeAsync()
        {
            return await _gameObjectService.IsWarModeAsync(await _charStatsService.GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);
        }

        public Task<uint> GetWarTargetIdAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetWarTarget);
        }

        public Task AttackAsync(uint objectId)
        {
            return Client.SendPacketAsync(PacketType.SCAttack, objectId);
        }
    }
}