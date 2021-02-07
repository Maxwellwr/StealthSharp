#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ITradeService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface ITradeService
    {
        Task<bool> GetIsTradeAsync();

        Task<byte> GetTradeCountAsync();

        Task<bool> CancelTradeAsync(byte tradeNum);

        Task ConfirmTradeAsync(byte tradeNum);

        Task<uint> GetTradeContainerAsync(byte tradeNum, byte num);

        Task<uint> GetTradeOpponentAsync(byte tradeNum);

        Task<string> GetTradeOpponentNameAsync(byte tradeNum);

        Task<bool> TradeCheckAsync(byte tradeNum, byte num);
    }
}