#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SkillSpellService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class SkillSpellService : BaseService, ISkillSpellService
    {
        private readonly ITargetService _targetService;
        private readonly IObjectSearchService _searchService;
        private readonly IGameObjectService _gameObjectService;
        private readonly ICharStatsService _charStatsService;
        private readonly Dictionary<string, int> _skills = new();

        public SkillSpellService(IStealthSharpClient client, 
            ITargetService targetService,
            IObjectSearchService searchService,
            IGameObjectService gameObjectService,
            ICharStatsService charStatsService)
            : base(client)
        {
            _targetService = targetService;
            _searchService = searchService;
            _gameObjectService = gameObjectService;
            _charStatsService = charStatsService;
        }

        public Task<uint> GetLastStatusAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastStatus);
        }

        public async Task<bool> CastAsync(string spellName)
        {
            if (!System.Enum.TryParse(spellName, out Spell val))
            {
                return false;
            }

            return await CastAsync(val);
        }

        public async Task<bool> CastAsync(Spell spell)
        {
            if (spell == Spell.None)
            {
                return false;
            }

            await Client.SendPacketAsync(PacketType.SCCastSpell, spell);
            return true;
        }

        public async Task<bool> CastSpellToObjAsync(string spellName, uint objId)
        {
            await _targetService.WaitTargetObjectAsync(objId);
            return await CastAsync(spellName);
        }

        public async Task<bool> CastSpellToObjAsync(Spell spell, uint objId)
        {
            await _targetService.WaitTargetObjectAsync(objId);
            return await CastAsync(spell);
        }

        public async Task<(bool result, int skillId)> GetSkillIdAsync(string skillName)
        {
            if (_skills.ContainsKey(skillName) && _skills.TryGetValue(skillName, out var skillId))
            {
                return (true, skillId);
            }

            skillId = await Client.SendPacketAsync<string, int>(PacketType.SCGetSkillID, skillName);
            if (skillId < 250)
            {
                _skills[skillName] = skillId;
                return (true, skillId);
            }

            return (false, 250);
        }

        public async Task<double> GetSkillCapAsync(string skillName)
        {
            var skillId = await GetSkillIdAsync(skillName);
            if (!skillId.result)
            {
                return -1;
            }

            return await Client.SendPacketAsync<int, double>(PacketType.SCGetSkillCap, skillId.skillId);
        }

        public async Task<double> GetSkillValueAsync(string skillName)
        {
            var skillId = await GetSkillIdAsync(skillName);
            if (!skillId.result)
            {
                return -1;
            }

            return await Client.SendPacketAsync<int, double>(PacketType.SCSkillValue, skillId.skillId);
        }

        public async Task<double> GetSkillCurrentValueAsync(string skillName)
        {
            var skillId = await GetSkillIdAsync(skillName);
            if (!skillId.result)
            {
                return -1;
            }

            return await Client.SendPacketAsync<int, double>(PacketType.SCSkillCurrentValue, skillId.skillId);
        }

        public async Task<bool> IsActiveSpellAbilityAsync(string spellName)
        {
            if (!System.Enum.TryParse(spellName, out Spell val))
            {
                return false;
            }

            return await CastAsync(val);
        }

        public Task ReqVirtuesGumpAsync()
        {
            return Client.SendPacketAsync(PacketType.SCReqVirtuesGump);
        }

        public Task SetStatStateAsync(byte statNum, byte statState)
        {
            return Client.SendPacketAsync(PacketType.SCChangeStatLockState, (statNum, statState));
        }

        public async Task SkillLockStateAsync(string skillName, byte skillState)
        {
            var skillId = await GetSkillIdAsync(skillName);
            if (skillId.result)
            {
                await Client.SendPacketAsync(PacketType.SCChangeSkillLockState, (skillId.skillId, skillState));
            }
            else
            {
                throw new ArgumentNullException(nameof(skillName), "Can't find skill with name: " + skillName);
            }
        }

        public Task ToggleFlyAsync()
        {
            return Client.SendPacketAsync(PacketType.SCToggleFly);
        }

        public Task UseOtherPaperdollScrollAsync(uint id)
        {
            return Client.SendPacketAsync(PacketType.SCUseOtherPaperdollScroll, id);
        }

        public Task UsePrimaryAbilityAsync()
        {
            return Client.SendPacketAsync(PacketType.SCUsePrimaryAbility);
        }

        public Task UseSecondaryAbilityAsync()
        {
            return Client.SendPacketAsync(PacketType.SCUseSecondaryAbility);
        }

        public Task<string> GetActiveAbilityAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetActiveAbility);
        }

        public Task UseSelfPaperdollScrollAsync()
        {
            return Client.SendPacketAsync(PacketType.SCUseSelfPaperdollScroll);
        }

        public async Task<bool> UseSkillAsync(string skillName)
        {
            var skillId=await GetSkillIdAsync(skillName);
            if (!skillId.result)
            {
                return false;
            }

            await Client.SendPacketAsync(PacketType.SCUseSkill, skillId.skillId);
            return true;
        }

        public async Task UseVirtueAsync(string virtueName)
        {
            if (virtueName.GetEnum(out Virtue virtue))
            {
                await UseVirtueAsync(virtue);
            }
        }

        public Task UseVirtueAsync(Virtue virtue)
        {
            return Client.SendPacketAsync(PacketType.SCUseVirtue, (uint) virtue);
        }

        public async Task BandageSelfAsync()
        {
            var bandages = await _searchService.FindTypeExAsync(0xE21, 0xFFFF, await _searchService.GetBackpackAsync() ,true);
            if (bandages > 0)
                await _gameObjectService.UseItemOnMobileAsync(bandages, await _charStatsService.GetSelfAsync());
        }

        public async Task<byte> GetSkillLockStateAsync(string skillName)
        {
            var skillId=await GetSkillIdAsync(skillName);
            if (!skillId.result)
            {
                throw new ArgumentNullException(nameof(skillName), "Can't find skill with name: " + skillName);
            }

            return await Client.SendPacketAsync<int, byte>(PacketType.SCGetSkillLockState, skillId.skillId);
        }

        public Task<bool> IsActiveSpellAbility(string spellName)
        {
            if (!System.Enum.TryParse(spellName, out Spell val))
            {
                throw new ArgumentNullException(nameof(spellName), "Can't find spell with name: " + spellName);
            }

            return IsActiveSpellAbility(val);
        }
        
        public Task<bool> IsActiveSpellAbility(Spell spell)
        {
            return Client.SendPacketAsync<Spell, bool>(PacketType.SCIsActiveSpellAbility, spell);
        }
    }
}