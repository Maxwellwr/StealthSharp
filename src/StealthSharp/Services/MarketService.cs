#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MarketService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class MarketService : BaseService, IMarketService
    {
        public MarketService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task SetAutoBuyDelayAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetAutoBuyDelay, value);
        }

        public Task<ushort> GetAutoBuyDelayAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetAutoBuyDelay);
        }

        public Task SetAutoSellDelayAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetAutoSellDelay, value);
        }

        public Task<ushort> GetAutoSellDelayAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetAutoSellDelay);
        }

        public Task<List<string>> GetShopListAsync()
        {
            return Client.SendPacketAsync<List<string>>(PacketType.SCGetShopList);
        }

        public Task AutoBuyAsync(ushort itemType, ushort itemColor, ushort quantity)
        {
             return Client.SendPacketAsync(PacketType.SCAutoBuy, (itemType, itemColor, quantity));
        }

        public Task AutoBuyExAsync(ushort itemType, ushort itemColor, ushort quantity, uint price, string name)
        {
            return Client.SendPacketAsync(PacketType.SCAutoBuyEx, (itemType, itemColor, quantity, price, name));
        }

        public Task AutoSellAsync(ushort itemType, ushort itemColor, ushort quantity)
        {
            return Client.SendPacketAsync(PacketType.SCAutoSell, (itemType, itemColor, quantity));
        }

        public Task ClearShopListAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClearShopList);
        }
    }
}