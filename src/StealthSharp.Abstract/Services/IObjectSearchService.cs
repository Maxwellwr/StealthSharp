#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IObjectSearchService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Model;

namespace StealthSharp.Services
{
    public interface IObjectSearchService
    {
        /// <summary>
        ///     Returns the char - ID backpack(Backpack ID).
        ///     If there is no connection to the UO server - returns 0.
        ///     Often used, for example, in the search, index recipient to drag things on and so forth.
        /// </summary>
        Task<uint> GetBackpackAsync();

        Task<int> GetFindCountAsync();

        void SetFindDistance(uint value);
        Task<uint> GetFindDistanceAsync();

        Task<List<uint>> GetFindedListAsync();

        Task<int> GetFindFullQuantityAsync();

        void SetFindInNulPoint(bool value);
        Task<bool> GetFindInNulPointAsync();

        Task<uint> GetFindItemAsync();

        Task<int> GetFindQuantityAsync();

        void SetFindVertical(int value);
        Task<int> GetFindVerticalAsync();

        /// <summary>
        ///     A pointer to the ground. Often used, for example, in the search.
        ///     If there is no connection to the UO server - returns 0.
        /// </summary>
        Task<uint> GetGroundAsync();

        Task<List<uint>> GetIgnoreListAsync();

        Task<uint> GetLastContainerAsync();

        Task<uint> GetLastObjectAsync();

        Task<int> CountAsync(ushort objType);

        Task<int> CountExAsync(ushort objType, ushort color, uint container);

        Task<int> CountGroundAsync(ushort objType);

        Task<uint> FindAtCoordAsync(ushort x, ushort y);

        Task<uint> FindNotorietyAsync(ushort objType, byte notoriety);

        Task<uint> FindTypeAsync(ushort objType, uint container);

        Task<uint> FindTypeExAsync(ushort objType, ushort color, uint container, bool inSub);

        Task<uint> FindTypesArrayExAsync(ushort[] objTypes, ushort[] colors, uint[] containers, bool inSub);

        Task<List<MultiItem>> GetMultisAsync();

        void Ignore(uint objId);

        void IgnoreOff(uint objId);

        void IgnoreReset();
    }
}