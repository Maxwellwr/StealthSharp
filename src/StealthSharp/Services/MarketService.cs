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
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class MarketService : BaseService, IMarketService
    {
        public MarketService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public void SetAutoBuyDelay(ushort value)
        {
            Client.SendPacket(PacketType.SCSetAutoBuyDelay, value);
        }

        public Task<ushort> GetAutoBuyDelayAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetAutoBuyDelay);
        }

        public void SetAutoSellDelay(ushort value)
        {
            Client.SendPacket(PacketType.SCSetAutoSellDelay, value);
        }

        public Task<ushort> GetAutoSellDelayAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetAutoSellDelay);
        }

        public Task<List<string>> GetShopListAsync()
        {
            return Client.SendPacketAsync<List<string>>(PacketType.SCGetShopList);
        }

        public void AutoBuy(ushort itemType, ushort itemColor, ushort quantity)
        {
             Client.SendPacket(PacketType.SCAutoBuy, (itemType, itemColor, quantity));
        }

        public void AutoBuyEx(ushort itemType, ushort itemColor, ushort quantity, uint price, string name)
        {
            Client.SendPacket(PacketType.SCAutoBuyEx, (itemType, itemColor, quantity, price, name));
        }

        public void AutoSell(ushort itemType, ushort itemColor, ushort quantity)
        {
            Client.SendPacket(PacketType.SCAutoSell, (itemType, itemColor, quantity));
        }

        public void ClearShopList()
        {
            Client.SendPacket(PacketType.SCClearShopList);
        }
    }
}