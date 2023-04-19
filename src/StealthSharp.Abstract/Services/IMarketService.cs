#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMarketService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace StealthSharp.Services
{
    public interface IMarketService
    {
        Task SetAutoBuyDelayAsync(ushort value);
        Task<ushort> GetAutoBuyDelayAsync();

        Task SetAutoSellDelayAsync(ushort value);
        Task<ushort> GetAutoSellDelayAsync();

        Task<List<string>> GetShopListAsync();

        Task AutoBuyAsync(ushort itemType, ushort itemColor, ushort quantity);

        Task AutoBuyExAsync(ushort itemType, ushort itemColor, ushort quantity, uint price, string name);

        Task AutoSellAsync(ushort itemType, ushort itemColor, ushort quantity);

        Task ClearShopListAsync();
    }
}