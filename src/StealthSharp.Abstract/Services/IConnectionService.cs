#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IConnectionService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IConnectionService
    {
        Task SetARStatusAsync(bool value);
        Task<bool> GetARStatusAsync();

        Task<bool> GetConnectedAsync();

        /// <summary>
        ///     Returns last time connected to server(ConnectedTime).
        ///     Returns during the last successful connecting to the server.
        ///     If there is no connection to the UO server - return '30.12.1899'.
        /// </summary>
        Task<DateTime> GetConnectedTimeAsync();

        /// <summary>
        ///     Returns last time disconnected from server(DisconnectedTime).
        ///     returns the last time disconnect from server(for whatever reason).
        ///     In the event that such action did not occur - will return '30.12.1899'.
        /// </summary>
        Task<DateTime> GetDisconnectedTimeAsync();

        Task SetPauseScriptOnDisconnectStatusAsync(bool value);
        Task<bool> GetPauseScriptOnDisconnectStatusAsync();

        Task<string> GetGameServerIPStringAsync();

        Task<string> GetProxyIPAsync();

        Task<ushort> GetProxyPortAsync();

        Task<bool> GetUseProxyAsync();

        Task<int> ChangeProfileAsync(string name);

        Task<int> ChangeProfileAsync(string name, string shardName, string charName);

        Task<bool> CheckLagAsync(int timeoutMS);

        Task CheckLagBeginAsync();

        Task CheckLagEndAsync();

        Task ConnectAsync();

        Task DisconnectAsync();

        Task SetARExtParamsAsync(string shardName, string charName, bool useAtEveryConnect);
    }
}