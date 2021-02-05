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

        public SkillSpellService(IStealthSharpClient<ushort, uint, ushort> client, 
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

        public bool Cast(string spellName)
        {
            if (!System.Enum.TryParse(spellName, out Spell val))
            {
                return false;
            }

            return Cast(val);
        }

        public bool Cast(Spell spell)
        {
            if (spell == Spell.None)
            {
                return false;
            }

            Client.SendPacket(PacketType.SCCastSpell, spell);
            return true;
        }

        public bool CastSpellToObj(string spellName, uint objId)
        {
            _targetService.WaitTargetObject(objId);
            return Cast(spellName);
        }

        public bool CastSpellToObj(Spell spell, uint objId)
        {
            _targetService.WaitTargetObject(objId);
            return Cast(spell);
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
        
        public void ReqVirtuesGump()
        {
            Client.SendPacket(PacketType.SCReqVirtuesGump);
        }

        public void SetStatState(byte statNum, byte statState)
        {
            Client.SendPacket(PacketType.SCChangeStatLockState, (statNum, statState));
        }

        public async Task SetSkillLockStateAsync(string skillName, byte skillState)
        {
            var skillId = await GetSkillIdAsync(skillName);
            if (skillId.result)
            {
                Client.SendPacket(PacketType.SCChangeSkillLockState, (skillId.skillId, skillState));
            }
            else
            {
                throw new ArgumentNullException(nameof(skillName), "Can't find skill with name: " + skillName);
            }
        }

        public void ToggleFly()
        {
            Client.SendPacket(PacketType.SCToggleFly);
        }

        public void UseOtherPaperdollScroll(uint id)
        {
            Client.SendPacket(PacketType.SCUseOtherPaperdollScroll, id);
        }

        public void UsePrimaryAbility()
        {
            Client.SendPacket(PacketType.SCUsePrimaryAbility);
        }

        public void UseSecondaryAbility()
        {
            Client.SendPacket(PacketType.SCUseSecondaryAbility);
        }

        public Task<string> GetActiveAbilityAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetActiveAbility);
        }

        public void UseSelfPaperdollScroll()
        {
            Client.SendPacket(PacketType.SCUseSelfPaperdollScroll);
        }

        public async Task<bool> UseSkillAsync(string skillName)
        {
            var skillId=await GetSkillIdAsync(skillName);
            if (!skillId.result)
            {
                return false;
            }

            Client.SendPacket(PacketType.SCUseSkill, skillId.skillId);
            return true;
        }

        public void UseVirtue(string virtueName)
        {
            if (virtueName.GetEnum(out Virtue virtue))
            { UseVirtue(virtue);
            }
        }

        public void UseVirtue(Virtue virtue)
        {
            Client.SendPacket(PacketType.SCUseVirtue, (uint) virtue);
        }

        public async Task BandageSelfAsync()
        {
            var bandages = await _searchService.FindTypeExAsync(0xE21, 0xFFFF, await _searchService.GetBackpackAsync() ,true);
            if (bandages > 0)
                _gameObjectService.UseItemOnMobile(bandages, await _charStatsService.GetSelfAsync());
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

        public Task<bool> IsActiveSpellAbilityAsync(string spellName)
        {
            if (!System.Enum.TryParse(spellName, out Spell val))
            {
                throw new ArgumentNullException(nameof(spellName), "Can't find spell with name: " + spellName);
            }

            return IsActiveSpellAbilityAsync(val);
        }
        
        public Task<bool> IsActiveSpellAbilityAsync(Spell spell)
        {
            return Client.SendPacketAsync<Spell, bool>(PacketType.SCIsActiveSpellAbility, spell);
        }
    }
}