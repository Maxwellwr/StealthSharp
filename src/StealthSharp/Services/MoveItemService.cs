#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MoveItemService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class MoveItemService : BaseService, IMoveItemService
    {
        private readonly ICharStatsService _charStatsService;
        private readonly IGameObjectService _gameObjectService;
        private readonly IObjectSearchService _objectSearchService;

        public MoveItemService(
            IStealthSharpClient<ushort, uint, ushort> client,
            
            ICharStatsService charStatsService,
            IGameObjectService gameObjectService,
            IObjectSearchService objectSearchService)
            : base(client)
        {
            _charStatsService = charStatsService;
            _gameObjectService = gameObjectService;
            _objectSearchService = objectSearchService;
        }

        public void SetDropDelay(uint value)
        {
            Client.SendPacket(PacketType.SCSetDropDelay,
                value);
        }

        public Task<uint> GetDropDelayAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetDropDelay);
        }

        public void SetPickedUpItem(uint value)
        {
            Client.SendPacket(PacketType.SCSetPickupedItem,
                value);
        }

        public Task<uint> GetPickedUpItemAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetPickupedItem);
        }

        public void SetDropCheckCoord(bool value)
        {
            Client.SendPacket(PacketType.SCSetDropCheckCoord,
                value);
        }

        public Task<bool> GetDropCheckCoordAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetDropCheckCoord);
        }

        public async Task<bool> DragItemAsync(uint itemId, int count = 0)
        {
            var rescount = count;

            if (await _charStatsService.GetDeadAsync())
            {
                throw new InvalidOperationException("Error: " + nameof(DragItemAsync) + " [Character is dead]");
            }

            if (await GetPickedUpItemAsync() != 0 &&
                await _gameObjectService.IsObjectExistsAsync(await GetPickedUpItemAsync()))
            {
                throw new InvalidOperationException("Error: " + nameof(DragItemAsync) +
                                                    " [Must drop current item before dragging a new one]");
            }

            var quantity = await _gameObjectService.GetQuantityAsync(itemId);

            if (!await _gameObjectService.IsObjectExistsAsync(itemId))
            {
                throw new InvalidOperationException("Error: " + nameof(DragItemAsync) + " [Object not found]");
            }

            if (count <= 0 || count > quantity)
            {
                rescount = quantity;
            }

            Client.SendPacket(PacketType.SCDragItem,
                (itemId, rescount));

            return (await GetPickedUpItemAsync()) == itemId;
        }

        public async Task<bool> DropAsync(uint itemId, int count, int x, int y, int z)
        {
            return await MoveItemAsync(itemId, count, await _objectSearchService.GetGroundAsync(), x, y, z);
        }

        public Task<bool> DropHereAsync(uint itemId)
        {
            return DropAsync(itemId, 0, 0, 0, 0);
        }

        public Task<bool> DropItemAsync(uint moveIntoId, int x, int y, int z)
        {
            return Client.SendPacketAsync<(uint, int, int, int), bool>(PacketType.SCDropItem, (moveIntoId, x, y, z));
        }

        public Task<bool> EmptyContainerAsync(uint container, uint destContainer, ushort delayMS)
        {
            return MoveItemsExAsync(container, 0xFFFF, 0xFFFF, destContainer, 0xFFFF, 0xFFFF, 0, delayMS, 0);
        }

        public async Task<bool> GrabAsync(uint itemId, int count)
        {
            return await MoveItemAsync(itemId, count, await _objectSearchService.GetBackpackAsync(), 0, 0, 0);
        }

        public async Task<bool> MoveItemAsync(uint itemId, int count, uint moveIntoId, int x, int y, int z)
        {
            if (await DragItemAsync(itemId, count))
            {
                return await DropItemAsync(moveIntoId, x, y, z);
            }

            return false;
        }

        public Task<bool> MoveItemsAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId, int x,
            int y, int z,
            int delayMS)
        {
            return MoveItemsExAsync(container, itemsType, itemsColor, moveIntoId, x, y, z, delayMS, 0);
        }

        public async Task<bool> MoveItemsExAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId,
            int x, int y,
            int z, int delayMS, int maxItems)
        {
            int moveItemsCount;
            int beforeMoveCount;

            await _objectSearchService.FindTypeExAsync(itemsType, itemsColor, container, false);

            if ((await _objectSearchService.GetFindedListAsync()).Count == 0)
            {
                return false;
            }

            if (await GetDropDelayAsync() > delayMS)
            {
                delayMS = (int) (await GetDropDelayAsync());
            }

            beforeMoveCount = (await _objectSearchService.GetFindedListAsync()).Count;
            if (maxItems <= 0 || maxItems > beforeMoveCount)
            {
                moveItemsCount = beforeMoveCount;
            }
            else
            {
                moveItemsCount = maxItems;
            }

            for (var i = 0; i < moveItemsCount; i++)
            {
                var id = (await _objectSearchService.GetFindedListAsync())[i];
                await MoveItemAsync(id, 0, moveIntoId, x, y, z);
                Thread.Sleep(delayMS);
            }

            await _objectSearchService.FindTypeExAsync(itemsType, itemsColor, container, false);
            return (await _objectSearchService.GetFindedListAsync()).Count == beforeMoveCount - moveItemsCount;
        }

        public void SetCatchBag(uint objectId)
        {
            Client.SendPacket(PacketType.SCSetCatchBag,
                objectId);
        }

        public void UnsetCatchBag()
        {
            Client.SendPacket(PacketType.SCUnsetCatchBag);
        }
    }
}