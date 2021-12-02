#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ISkillSpellService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enumeration;

namespace StealthSharp.Services
{
    public interface ISkillSpellService
    {
        Task<uint> GetLastStatusAsync();

        Task<bool> CastAsync(string spellName);

        Task<bool> CastAsync(Spell spell);

        Task<bool> CastSpellToObjAsync(string spellName, uint objId);

        Task<bool> CastSpellToObjAsync(Spell spell, uint objId);

        Task<double> GetSkillCapAsync(string skillName);

        Task<(bool result, int skillId)> GetSkillIdAsync(string skillName);

        Task<double> GetSkillValueAsync(string skillName);

        Task<double> GetSkillCurrentValueAsync(string skillName);

        Task<bool> IsActiveSpellAbilityAsync(string spellName);

        Task ReqVirtuesGumpAsync();

        Task SetStatStateAsync(byte statNum, byte statState);

        Task SkillLockStateAsync(string skillName, byte skillState);

        Task ToggleFlyAsync();

        Task UseOtherPaperdollScrollAsync(uint id);

        Task UsePrimaryAbilityAsync();

        Task UseSecondaryAbilityAsync();

        Task UseSelfPaperdollScrollAsync();

        Task<bool> UseSkillAsync(string skillName);

        Task UseVirtueAsync(string virtueName);

        Task UseVirtueAsync(Virtue virtue);

        Task BandageSelfAsync();

        Task<byte> GetSkillLockStateAsync(string skillName);

        Task<bool> IsActiveSpellAbility(string spellName);
        
        Task<bool> IsActiveSpellAbility(Spell spell);
    }
}