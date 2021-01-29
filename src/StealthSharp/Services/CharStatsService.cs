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
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class CharStatsService : BaseService, ICharStatsService
    {
        private readonly IGameObjectService _gameObjectService;
        private uint _self;

        public CharStatsService(
            IStealthSharpClient<ushort, uint, ushort> client,
            IGameObjectService gameObjectService)
            : base(client)
        {
            _gameObjectService = gameObjectService;
        }

        public Task<bool> GetHiddenAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetHiddenStatus);
        }

        public Task<bool> GetParalyzedAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetParalyzedStatus);
        }

        public Task<bool> GetPoisonedAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetPoisonedStatus);
        }

        public Task<ushort> GetArmorAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfArmor);
        }

        public Task<string> GetCharNameAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetCharName);
        }

        public Task<string> GetCharTitleAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetCharTitle);
        }

        public Task<bool> GetDeadAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetDeadStatus);
        }

        public Task<int> GetDexAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfDex);
        }

        public Task<uint> GetGoldAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetSelfGold);
        }

        public Task<int> GetHPAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfLife);
        }

        public Task<int> GetIntAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfInt);
        }

        public Task<int> GetLifeAsync()
        {
            return GetHPAsync();
        }

        public Task<int> GetLuckAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfLuck);
        }

        public Task<int> GetManaAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfMana);
        }

        public Task<int> GetMaxHPAsync()
        {
            return GetMaxLifeAsync();
        }

        public Task<int> GetMaxLifeAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfMaxLife);
        }

        public Task<int> GetMaxManaAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfMaxMana);
        }

        public Task<int> GetMaxStamAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfMaxStam);
        }

        public Task<ushort> GetMaxWeightAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfMaxWeight);
        }

        public Task<byte> GetPetsCurrentAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetSelfPetsCurrent);
        }

        public Task<byte> GetPetsMaxAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetSelfPetsMax);
        }

        public Task<byte> GetRaceAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetSelfRace);
        }

        public Task<byte> GetSexAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetSelfSex);
        }

        public Task<int> GetStamAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfStam);
        }

        public Task<int> GetStrAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetSelfStr);
        }

        public Task<ushort> GetWeightAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfWeight);
        }

        public Task<ushort> GetColdResistAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfColdResist);
        }

        public Task<ushort> GetEnergyResistAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfEnergyResist);
        }

        public Task<ushort> GetFireResistAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfFireResist);
        }

        public Task<ushort> GetPoisonResistAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetSelfPoisonResist);
        }

        public Task<Point> GetQuestArrowAsync()
        {
            return Client.SendPacketAsync<Point>(PacketType.SCGetQuestArrow);
        }

        public async Task<uint> GetSelfAsync()
        {
            if (_self == 0)
            {
                _self = await Client.SendPacketAsync<uint>(PacketType.SCGetSelfID);
            }

            return _self;
        }

        public Task<uint> GetSelfHandleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<byte> GetWorldNumAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetWorldNum);
        }

        public Task<ExtendedInfo> GetExtendedInfoAsync()
        {
            return Client.SendPacketAsync<ExtendedInfo>(PacketType.SCGetExtInfo);
        }

        public Task<List<BuffIcon>> GetBuffBarInfoAsync()
        {
            return Client.SendPacketAsync<List<BuffIcon>>(PacketType.SCGetBuffBarInfo);
        }

        public async Task<ushort> GetXAsync()
        {
            return await _gameObjectService.GetXAsync(await GetSelfAsync());
        }

        public async Task<ushort> GetYAsync()
        {
            return await _gameObjectService.GetYAsync(await GetSelfAsync());
        }

        public async Task<sbyte> GetZAsync()
        {
            return await _gameObjectService.GetZAsync(await GetSelfAsync());
        }

        public Task<string> GetAltNameAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, string>(PacketType.SCGetAltName, objId);
        }

        public Task<uint> GetPriceAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, uint>(PacketType.SCGetPrice, objId);
        }

        public Task<string> GetTitleAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, string>(PacketType.SCGetTitle, objId);
        }

        public Task<byte> GetStatLockStateAsync(byte statNum)
        {
            return Client.SendPacketAsync<byte, byte>(PacketType.SCGetStatLockState, statNum);
        }
    }
}