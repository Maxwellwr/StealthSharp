#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ContextMenuService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class ContextMenuService : BaseService, IContextMenuService
    {
        public ContextMenuService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public void ClearContextMenu()
        {
            Client.SendPacket(PacketType.SCClearContextMenu);
        }

        public Task<List<string>> GetContextMenuAsync()
        {
            return Client.SendPacketAsync<List<string>>(PacketType.SCGetContextMenu);
        }

        public Task<ContextMenu> GetContextMenuRecAsync()
        {
            return Client.SendPacketAsync<ContextMenu>(PacketType.SCGetContextMenuRec);
        }

        public void RequestContextMenu(uint id)
        {
            Client.SendPacket(PacketType.SCRequestContextMenu, id);
        }

        public void SetContextMenuHook(uint menuId, byte entryNumber)
        {
            Client.SendPacket(PacketType.SCSetContextMenuHook, (menuId, entryNumber));
        }
    }
}