#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ICharStatsService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface ICharStatsService
    {
        /// <summary>
        ///     Returns the char - Stealth(Hidden).
        ///     If there is no connection to the UO server - returns False.
        /// </summary>
        Task<bool> GetHiddenAsync();

        /// <summary>
        ///     Returns the char - paralysis(Paralyzed).
        ///     If there is no connection to the UO server - returns False.
        /// </summary>
        Task<bool> GetParalyzedAsync();

        /// <summary>
        ///     Возвращает параметр чара - Отравленность (Poisoned).
        ///     В случае, если отсутствует соединение с UO сервером - вернет False.
        /// </summary>
        Task<bool> GetPoisonedAsync();

        /// <summary>
        ///     Returns the char - number of "units" of armor(Armor).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<ushort> GetArmorAsync();

        /// <summary>
        ///     Returns the chararacter name.
        ///     If there is no connection to the UO server - returns an empty string().
        /// </summary>
        Task<string> GetCharNameAsync();

        /// <summary>
        ///     Returns the character title.
        ///     If there is no connection to the UO server - returns an empty string().
        /// </summary>
        Task<string> GetCharTitleAsync();

        /// <summary>
        ///     Returns the character dead status
        ///     If True - dead, if False - alive
        ///     If there is no connection to the UO server - returns False.
        /// </summary>
        Task<bool> GetDeadAsync();

        /// <summary>
        ///     Returns the character agility(DEX).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetDexAsync();

        /// <summary>
        ///     Returns the char - count the money in the pack(Gold amount).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<uint> GetGoldAsync();

        /// <summary>
        ///     Same as <see cref="GetLifeAsync" />. Returns the char - healthy(HITS).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetHPAsync()
        {
            return GetLifeAsync();
        }

        /// <summary>
        ///     Returns the player's - intelligence(INT).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetIntAsync();

        /// <summary>
        ///     Same as <see cref="GetHPAsync" />. Returns the char - healthy(HITS).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetLifeAsync();

        /// <summary>
        ///     Returns the char - Luck(Luck).
        ///     This only works from the client version Samurie Empire + on the server must be enabled advanced stats sent to the
        ///     client, otherwise returns 0.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetLuckAsync();

        /// <summary>
        ///     Returns the char - Man(Mana points).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetManaAsync();

        /// <summary>
        ///     Same as <see cref="GetMaxLifeAsync" />. In 99% of cases as well <see cref="GetStrAsync" />. Can differ only if the admin changes
        ///     the parameters specifically object(char, SPC) and puts his hands MaxHP other than Str(usually in a big way).
        ///     Returns the char - the maximum number of health(Max HITS).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetMaxHPAsync()
        {
            return GetMaxLifeAsync();
        }

        /// <summary>
        ///     Same as <see cref="GetMaxHPAsync" />. In 99% of cases as well <see cref="GetStrAsync" />. Can differ only if the admin changes the
        ///     parameters specifically object(char, SPC) and puts his hands MaxHP other than Str(usually in a big way).
        ///     Returns the char - the maximum number of health(Max HITS).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetMaxLifeAsync();

        /// <summary>
        ///     In 99% of the same <see cref="GetIntAsync" />. Can differ only if the admin changes the parameters specifically
        ///     object(char, SPC) and puts his hands MaxMana different from Int(usually in a big way).
        ///     Returns the char - the maximum amount of mana(Max Mana).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetMaxManaAsync();

        /// <summary>
        ///     In 99% of the same <see cref="GetDexAsync" />. Can differ only if the admin changes the parameters specifically
        ///     object(char, SPC) and puts his hands MaxStam different from Dex(usually in a big way).
        ///     Returns the chara - the maximum amount of stamina(Max Stamina).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<int> GetMaxStaminaAsync();

        /// <summary>
        ///     Returns the char - Maximum Weight(Weight).
        ///     This only works from the client version Samurie Empire + on the server must be enabled advanced stats sent to the
        ///     client, otherwise returns 0.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<ushort> GetMaxWeightAsync();

        /// <summary>
        ///     Returns the char - the number of animals(Pets).
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<byte> GetPetsCurrentAsync();

        /// <summary>
        ///     Returns the character's maximum number of animals (Pets Maximum).
        ///     If there is no connection with the UO server - returns 0.
        /// </summary>
        Task<byte> GetPetsMaxAsync();

        /// <summary>
        ///     Returns the character - Race (Race).
        ///     Works only from client version Samuri Empire + server must be included race, otherwise return 0.
        ///     Value: 0=Human, 1=Elf (on some servers, these default values can be changed or expanded)
        ///     If there is no connection with the UO server - returns 0.
        /// </summary>
        Task<byte> GetRaceAsync();

        /// <summary>
        ///     Returns the character - Gender (Sex).
        ///     If 0 is returned - male, if 1 is returned - female
        ///     If there is no connection with the UO server - returns 0.
        /// </summary>
        Task<byte> GetSexAsync();

        /// <summary>
        ///     Возвращает параметр чара - стамина (Stamina).
        ///     В случае, если отсутствует соединение с UO сервером - вернет 0.
        /// </summary>
        Task<int> GetStaminaAsync();

        /// <summary>
        ///     Возвращает параметр чара - сила (STR).
        ///     В случае, если отсутствует соединение с UO сервером - вернет 0.
        /// </summary>
        Task<int> GetStrAsync();

        /// <summary>
        ///     Возвращает параметр чара - Вес (Weight).
        ///     В случае, если отсутствует соединение с UO сервером - вернет 0.
        /// </summary>
        Task<ushort> GetWeightAsync();

        /// <summary>
        ///     Returns the char - cold resistance(Cold Resist).
        ///     This only works from the client version Samurie Empire + on the server must be enabled advanced stats sent to the
        ///     client, otherwise returns 0.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<ushort> GetColdResistAsync();

        /// <summary>
        ///     Returns the char - resistance to energy(Energy Resist).
        ///     This only works from the client version Samurie Empire + on the server must be enabled advanced stats sent to the
        ///     client, otherwise returns 0.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<ushort> GetEnergyResistAsync();

        /// <summary>
        ///     Returns the char - refractoriness(Fire Resist).
        ///     This only works from the client version Samurie Empire + on the server must be enabled advanced stats sent to the
        ///     client, otherwise returns 0.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<ushort> GetFireResistAsync();

        /// <summary>
        ///     Возвращает параметр чара - сопротивление яду (Poison Resist).
        ///     This only works from the client version Samurie Empire + on the server must be enabled advanced stats sent to the
        ///     client, otherwise returns 0.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<ushort> GetPoisonResistAsync();

        /// <summary>
        ///     Возвращает параметр чара - ID чара (Char ID).
        ///     В случае, если отсутствует соединение с UO сервером - вернет 0.
        /// </summary>
        Task<uint> GetSelfAsync();

        Task<WorldPoint> GetQuestArrowAsync();

        /// <summary>
        ///     Returns the number of the Map of the current character.
        ///     This works on servers where there is more than one map.
        ///     Default values ​​are:
        ///     0 - Felucca (Britannia)
        ///     1 - Trammel (Britannia_alt)
        ///     2 - Ilshenar
        ///     3 - Malas
        ///     4 - Tokuno
        ///     More values are possible for shards with SA support.
        ///     If there is no connection with the UO server - returns 0.
        /// </summary>
        Task<byte> GetWorldNumAsync();

        /// <summary>
        ///     Returns extended info of char in KR++ version of UO.
        /// </summary>
        Task<ExtendedInfo> GetExtendedInfoAsync();

        Task<List<BuffIcon>> GetBuffBarInfoAsync();

        Task<WorldPoint3D> GetPosition3DAsync();

        Task<WorldPoint> GetPositionAsync();

        Task<ushort> GetXAsync();

        Task<ushort> GetYAsync();

        Task<sbyte> GetZAsync();

        Task<string> GetAltNameAsync(uint objId);

        Task<uint> GetPriceAsync(uint objId);

        Task<string> GetTitleAsync(uint objId);

        Task<byte> GetStatLockStateAsync(byte statNum);
    }
}