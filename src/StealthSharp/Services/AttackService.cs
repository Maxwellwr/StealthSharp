#region Copyright

// -----------------------------------------------------------------------
// <copyright file="AttackService.cs" company="StealthSharp">
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
    public class AttackService : BaseService, IAttackService
    {
        private readonly ICharStatsService _charStatsService;
        private readonly IGameObjectService _gameObjectService;
        

        public AttackService(IStealthSharpClient<ushort, uint, ushort> client, 
            
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

        public void SetWarMode(bool value)
        {
            Client.SendPacket(PacketType.SCSetWarMode, value);
        }

        public async Task<bool> GetWarModeAsync()
        {
            return await _gameObjectService.IsWarModeAsync(await _charStatsService.GetSelfAsync());
        }

        public Task<uint> GetWarTargetIDAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetWarTarget);
        }

        public void Attack(uint objectId)
        {
            Client.SendPacket(PacketType.SCAttack, objectId);
        }
    }
}