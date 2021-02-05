#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ISkillSpellService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;

namespace StealthSharp.Services
{
    public interface ISkillSpellService
    {
        Task<uint> GetLastStatusAsync();

        bool Cast(string spellName);

        bool Cast(Spell spell);

        bool CastSpellToObj(string spellName, uint objId);

        bool CastSpellToObj(Spell spell, uint objId);

        Task<double> GetSkillCapAsync(string skillName);

        Task<(bool result, int skillId)> GetSkillIdAsync(string skillName);

        Task<double> GetSkillValueAsync(string skillName);

        Task<double> GetSkillCurrentValueAsync(string skillName);

        void ReqVirtuesGump();

        void SetStatState(byte statNum, byte statState);

        Task SetSkillLockStateAsync(string skillName, byte skillState);

        void ToggleFly();

        void UseOtherPaperdollScroll(uint iD);

        void UsePrimaryAbility();

        void UseSecondaryAbility();

        void UseSelfPaperdollScroll();

        Task<bool> UseSkillAsync(string skillName);

        void UseVirtue(string virtueName);

        void UseVirtue(Virtue virtue);

        Task BandageSelfAsync();

        Task<byte> GetSkillLockStateAsync(string skillName);

        Task<bool> IsActiveSpellAbilityAsync(string spellName);
        
        Task<bool> IsActiveSpellAbilityAsync(Spell spell);
    }
}