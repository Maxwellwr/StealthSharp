#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MenuService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class MenuService : BaseService, IMenuService
    {
        public MenuService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<List<string>> GetLastMenuItemsAsync()
        {
            return Client.SendPacketAsync<List<string>>(PacketType.SCGetLastMenuItems);
        }

        public Task<bool> GetMenuHookPresentAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCMenuHookPresent);
        }

        public Task<bool> GetMenuPresentAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCMenuPresent);
        }

        public Task AutoMenuAsync(string menuCaption, string elementCaption)
        {
            return Client.SendPacketAsync(PacketType.SCAutoMenu, (menuCaption, elementCaption));
        }

        public Task CancelMenuAsync()
        {
            return Client.SendPacketAsync(PacketType.SCCancelMenu);
        }

        public Task CloseMenuAsync()
        {
            return Client.SendPacketAsync(PacketType.SCCloseMenu);
        }

        public Task<List<string>> GetMenuItemsAsync(string menuCaption)
        {
            return Client.SendPacketAsync<string, List<string>>(PacketType.SCGetMenuItems,
                menuCaption);
        }

        public Task<List<MenuItem>> GetMenuItemsExAsync(string menuCaption)
        {
            return Client.SendPacketAsync<string, List<MenuItem>>(PacketType.SCGetMenuItemsEx, menuCaption);
        }

        public Task WaitMenuAsync(string menuCaption, string elementCaption)
        {
            return Client.SendPacketAsync(PacketType.SCWaitMenu, (menuCaption, elementCaption));
        }

        public async Task<bool> WaitForMenuPresentAsync(int timeout)
        {
            var endTime = DateTime.Now.AddMilliseconds(timeout);
            while (!await GetMenuPresentAsync().ConfigureAwait(false) && DateTime.Now < endTime) Thread.Sleep(10);

            return DateTime.Now < endTime && await GetMenuPresentAsync().ConfigureAwait(false);
        }
    }
}