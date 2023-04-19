#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TradeService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class TradeService : BaseService, ITradeService
    {
        public TradeService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<bool> GetIsTradeAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCCheckTradeState);
        }

        public Task<byte> GetTradeCountAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetTradeCount);
        }

        public Task<bool> CancelTradeAsync(byte tradeNum)
        {
            return Client.SendPacketAsync<byte, bool>(PacketType.SCCancelTrade, tradeNum);
        }

        public Task ConfirmTradeAsync(byte tradeNum)
        {
            return Client.SendPacketAsync(PacketType.SCConfirmTrade, tradeNum);
        }

        public Task<uint> GetTradeContainerAsync(byte tradeNum, byte num)
        {
            return Client.SendPacketAsync<(byte, byte), uint>(PacketType.SCGetTradeContainer, (tradeNum, num));
        }

        public Task<uint> GetTradeOpponentAsync(byte tradeNum)
        {
            return Client.SendPacketAsync<byte, uint>(PacketType.SCGetTradeOpponent, tradeNum);
        }

        public Task<string> GetTradeOpponentNameAsync(byte tradeNum)
        {
            return Client.SendPacketAsync<byte, string>(PacketType.SCGetTradeOpponentName, tradeNum);
        }

        public Task<bool> TradeCheckAsync(byte tradeNum, byte num)
        {
            return Client.SendPacketAsync<(byte, byte), bool>(PacketType.SCTradeCheck, (tradeNum, num));
        }
    }
}