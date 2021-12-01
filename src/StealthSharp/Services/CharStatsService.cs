#region Copyright

// -----------------------------------------------------------------------
// <copyright file="CharStatsService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class CharStatsService : BaseService, ICharStatsService
    {
        private readonly IGameObjectService _gameObjectService;
        private uint _self;

        public CharStatsService(
            IStealthSharpClient client,
            IGameObjectService gameObjectService)
            : base(client) =>
            _gameObjectService = gameObjectService;

        public Task<bool> GetHiddenAsync() => Client.SendPacketAsync<bool>(PacketType.SCGetHiddenStatus);

        public Task<bool> GetParalyzedAsync() => Client.SendPacketAsync<bool>(PacketType.SCGetParalyzedStatus);

        public Task<bool> GetPoisonedAsync() => Client.SendPacketAsync<bool>(PacketType.SCGetPoisonedStatus);

        public Task<ushort> GetArmorAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfArmor);

        public Task<string> GetCharNameAsync() => Client.SendPacketAsync<string>(PacketType.SCGetCharName);

        public Task<string> GetCharTitleAsync() => Client.SendPacketAsync<string>(PacketType.SCGetCharTitle);

        public Task<bool> GetDeadAsync() => Client.SendPacketAsync<bool>(PacketType.SCGetDeadStatus);

        public Task<int> GetDexAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfDex);

        public Task<uint> GetGoldAsync() => Client.SendPacketAsync<uint>(PacketType.SCGetSelfGold);

        public Task<int> GetLifeAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfLife);

        public Task<int> GetIntAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfInt);

        public Task<int> GetLuckAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfLuck);

        public Task<int> GetManaAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfMana);

        public Task<int> GetMaxLifeAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfMaxLife);

        public Task<int> GetMaxManaAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfMaxMana);

        public Task<int> GetMaxStaminaAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfMaxStam);

        public Task<ushort> GetMaxWeightAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfMaxWeight);

        public Task<byte> GetPetsCurrentAsync() => Client.SendPacketAsync<byte>(PacketType.SCGetSelfPetsCurrent);

        public Task<byte> GetPetsMaxAsync() => Client.SendPacketAsync<byte>(PacketType.SCGetSelfPetsMax);

        public Task<byte> GetRaceAsync() => Client.SendPacketAsync<byte>(PacketType.SCGetSelfRace);

        public Task<byte> GetSexAsync() => Client.SendPacketAsync<byte>(PacketType.SCGetSelfSex);

        public Task<int> GetStaminaAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfStam);

        public Task<int> GetStrAsync() => Client.SendPacketAsync<int>(PacketType.SCGetSelfStr);

        public Task<ushort> GetWeightAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfWeight);

        public Task<ushort> GetColdResistAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfColdResist);

        public Task<ushort> GetEnergyResistAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfEnergyResist);

        public Task<ushort> GetFireResistAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfFireResist);

        public Task<ushort> GetPoisonResistAsync() => Client.SendPacketAsync<ushort>(PacketType.SCGetSelfPoisonResist);

        public Task<WorldPoint> GetQuestArrowAsync() => Client.SendPacketAsync<WorldPoint>(PacketType.SCGetQuestArrow);

        public async Task<uint> GetSelfAsync()
        {
            if (_self == 0)
            {
                _self = await Client.SendPacketAsync<uint>(PacketType.SCGetSelfID).ConfigureAwait(false);
            }

            return _self;
        }

        public Task<uint> GetSelfHandleAsync() => throw new NotImplementedException();

        public Task<byte> GetWorldNumAsync() => Client.SendPacketAsync<byte>(PacketType.SCGetWorldNum);

        public Task<ExtendedInfo> GetExtendedInfoAsync() => Client.SendPacketAsync<ExtendedInfo>(PacketType.SCGetExtInfo);

        public Task<List<BuffIcon>> GetBuffBarInfoAsync() => Client.SendPacketAsync<List<BuffIcon>>(PacketType.SCGetBuffBarInfo);

        public async Task<ushort> GetXAsync() => await _gameObjectService.GetXAsync(await GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);

        public async Task<ushort> GetYAsync() => await _gameObjectService.GetYAsync(await GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);

        public async Task<sbyte> GetZAsync() => await _gameObjectService.GetZAsync(await GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);

        public Task<string> GetAltNameAsync(uint objId) => Client.SendPacketAsync<uint, string>(PacketType.SCGetAltName, objId);

        public Task<uint> GetPriceAsync(uint objId) => Client.SendPacketAsync<uint, uint>(PacketType.SCGetPrice, objId);

        public Task<string> GetTitleAsync(uint objId) => Client.SendPacketAsync<uint, string>(PacketType.SCGetTitle, objId);

        public Task<byte> GetStatLockStateAsync(byte statNum) => Client.SendPacketAsync<byte, byte>(PacketType.SCGetStatLockState, statNum);
    }
}