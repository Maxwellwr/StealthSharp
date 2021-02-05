#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMarketService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IMarketService
    {
        void SetAutoBuyDelay(ushort value);
        Task<ushort> GetAutoBuyDelayAsync();

        void SetAutoSellDelay(ushort value);
        Task<ushort> GetAutoSellDelayAsync();

        Task<List<string>> GetShopListAsync();

        void AutoBuy(ushort itemType, ushort itemColor, ushort quantity);

        void AutoBuyEx(ushort itemType, ushort itemColor, ushort quantity, uint price, string name);

        void AutoSell(ushort itemType, ushort itemColor, ushort quantity);

        void ClearShopList();
    }
}