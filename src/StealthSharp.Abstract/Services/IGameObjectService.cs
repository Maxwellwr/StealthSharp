#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IGameObjectService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface IGameObjectService
    {
        Task ClickOnObjectAsync(uint objId);

        Task<ushort> GetColorAsync(uint objId);

        Task<string> GetClilocAsync(uint objId);

        Task<string> GetClilocByIdAsync(uint clilocId);

        Task<int> GetDexAsync(uint objId);

        Task<byte> GetDirectionAsync(uint objId);

        Task<int> GetDistanceAsync(uint objId);

        Task<int> GetHPAsync(uint objId);

        Task<int> GetIntAsync(uint objId);

        Task<int> GetManaAsync(uint objId);

        Task<int> GetMaxHPAsync(uint objId);

        Task<int> GetMaxManaAsync(uint objId);

        Task<int> GetMaxStaminaAsync(uint objId);

        Task<string> GetNameAsync(uint objId);

        Task<byte> GetNotorietyAsync(uint objId);

        Task<uint> GetParentAsync(uint objId);

        Task<int> GetQuantityAsync(uint objId);

        Task<int> GetStaminaAsync(uint objId);

        Task<Image?> GetStaticArtAsync(uint id, ushort hue);

        Task<int> GetStrAsync(uint objId);

        Task<string> GetTooltipAsync(uint objId);

        /// <summary>
        ///     This function will return the tooltip of an item with the records that composes it.
        /// </summary>
        /// <example>
        /// IGameObjectService _gameObject;
        /// IObjectSearchService _objectSearch;
        /// IJournalService _journal;
        /// var aa := await _gameObject.GetToolTipRecAsync(await _objectSearch.GetBackpackAsync());
        /// await _journal.AddToSystemJournalAsync("Total lines in Toolptip: {0}", aa.Count));
        /// if (aa.count > 0)
        /// {
        ///     for (int i = 0, i &lt; aa.Count; i++)
        ///     {
        ///         await _journal.AddToSystemJournalAsync("Line {0}:", i);
        ///         var bb= aa.Items[i];
        ///         await _journal.AddToSystemJournalAsync("Cliloc: ${0:X8}", bb.ClilocID);
        ///         await _journal.AddToSystemJournalAsync("Cliloc text: {0}", await _gameObject.GetClilocByIDAsync( bb.ClilocID));
        ///         for ( int k= 0, k &lt; bb.Params.Length; k++)
        ///             await _journal.AddToSystemJournalAsync("Param-{0}: {1}" , k, bb.Params[ k]);
        ///     }
        /// }
        /// </example>
        /// <param name="objId">Item id.</param>
        /// <returns>List of tooltip records.</returns>
        Task<List<ClilocItemRec>> GetTooltipRecAsync(uint objId);

        Task<ushort> GetTypeAsync(uint objId);

        Task<ushort> GetXAsync(uint objId);

        Task<ushort> GetYAsync(uint objId);

        Task<sbyte> GetZAsync(uint objId);

        Task<bool> IsContainerAsync(uint objId);

        Task<bool> IsDeadAsync(uint objId);

        Task<bool> IsFemaleAsync(uint objId);
        
        Task<bool> IsHouseAsync(uint objId);

        Task<bool> IsHiddenAsync(uint objId);

        Task<bool> IsMovableAsync(uint objId);

        Task<bool> IsNPCAsync(uint objId);

        Task<bool> IsObjectExistsAsync(uint objId);

        Task<bool> IsParalyzedAsync(uint objId);

        Task<bool> IsPoisonedAsync(uint objId);

        Task<bool> IsRunningAsync(uint objId);

        Task<bool> IsWarModeAsync(uint objId);

        Task<bool> IsYellowHitsAsync(uint objId);

        Task<bool> MobileCanBeRenamedAsync(uint mobId);

        Task RenameMobileAsync(uint mobId, string newName);

        Task<uint> UseFromGroundAsync(ushort objType, ushort color);

        Task UseObjectAsync(uint objectId);

        Task<uint> UseTypeAsync(ushort objType, ushort color);

        Task UseItemOnMobileAsync(uint itemId, uint mobileId);
    }
}