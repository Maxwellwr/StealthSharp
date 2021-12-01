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
using StealthSharp.Enumeration;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class MoveItemService : BaseService, IMoveItemService
    {
        private readonly ICharStatsService _charStatsService;
        private readonly IGameObjectService _gameObjectService;
        private readonly IObjectSearchService _objectSearchService;

        public MoveItemService(
            IStealthSharpClient client,
            
            ICharStatsService charStatsService,
            IGameObjectService gameObjectService,
            IObjectSearchService objectSearchService)
            : base(client)
        {
            _charStatsService = charStatsService;
            _gameObjectService = gameObjectService;
            _objectSearchService = objectSearchService;
        }

        public Task SetDropDelayAsync(uint value)
        {
            return Client.SendPacketAsync(PacketType.SCSetDropDelay,
                value);
        }

        public Task<uint> GetDropDelayAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetDropDelay);
        }

        public Task SetPickedUpItemAsync(uint value)
        {
            return Client.SendPacketAsync(PacketType.SCSetPickupedItem,
                value);
        }

        public Task<uint> GetPickedUpItemAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetPickupedItem);
        }

        public Task SetDropCheckCoordAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetDropCheckCoord,
                value);
        }

        public Task<bool> GetDropCheckCoordAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetDropCheckCoord);
        }

        public async Task<bool> DragItemAsync(uint itemId, int count = 0)
        {
            var rescount = count;

            if (await _charStatsService.GetDeadAsync().ConfigureAwait(false))
            {
                throw new InvalidOperationException("Error: " + nameof(DragItemAsync) + " [Character is dead]");
            }

            if (await GetPickedUpItemAsync().ConfigureAwait(false) != 0 &&
                await _gameObjectService.IsObjectExistsAsync(await GetPickedUpItemAsync().ConfigureAwait(false)).ConfigureAwait(false))
            {
                throw new InvalidOperationException("Error: " + nameof(DragItemAsync) +
                                                    " [Must drop current item before dragging a new one]");
            }

            var quantity = await _gameObjectService.GetQuantityAsync(itemId).ConfigureAwait(false);

            if (!await _gameObjectService.IsObjectExistsAsync(itemId).ConfigureAwait(false))
            {
                throw new InvalidOperationException("Error: " + nameof(DragItemAsync) + " [Object not found]");
            }

            if (count <= 0 || count > quantity)
            {
                rescount = quantity;
            }

            await Client.SendPacketAsync(PacketType.SCDragItem,
                (itemId, rescount)).ConfigureAwait(false);

            return (await GetPickedUpItemAsync().ConfigureAwait(false)) == itemId;
        }

        public async Task<bool> DropAsync(uint itemId, int count, int x, int y, int z)
        {
            return await MoveItemAsync(itemId, count, await _objectSearchService.GetGroundAsync().ConfigureAwait(false), x, y, z).ConfigureAwait(false);
        }

        public Task<bool> DropHereAsync(uint itemId)
        {
            return DropAsync(itemId, 0, 0, 0, 0);
        }

        public Task<bool> DropItemAsync(uint moveIntoId, int x, int y, int z)
        {
            return Client.SendPacketAsync<(uint, int, int, int), bool>(PacketType.SCDropItem, (moveIntoId, x, y, z));
        }

        public Task<bool> EmptyContainerAsync(uint container, uint destContainer, ushort delayMs)
        {
            return MoveItemsExAsync(container, 0xFFFF, 0xFFFF, destContainer, 0xFFFF, 0xFFFF, 0, delayMs, 0);
        }

        public async Task<bool> GrabAsync(uint itemId, int count)
        {
            return await MoveItemAsync(itemId, count, await _objectSearchService.GetBackpackAsync().ConfigureAwait(false), 0, 0, 0).ConfigureAwait(false);
        }

        public async Task<bool> MoveItemAsync(uint itemId, int count, uint moveIntoId, int x, int y, int z)
        {
            if (await DragItemAsync(itemId, count).ConfigureAwait(false))
            {
                return await DropItemAsync(moveIntoId, x, y, z).ConfigureAwait(false);
            }

            return false;
        }

        public Task<bool> MoveItemsAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId, int x,
            int y, int z,
            int delayMs)
        {
            return MoveItemsExAsync(container, itemsType, itemsColor, moveIntoId, x, y, z, delayMs, 0);
        }

        public async Task<bool> MoveItemsExAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId,
            int x, int y,
            int z, int delayMs, int maxItems)
        {
            int moveItemsCount;
            int beforeMoveCount;

            await _objectSearchService.FindTypeExAsync(itemsType, itemsColor, container, false).ConfigureAwait(false);

            if ((await _objectSearchService.GetFindedListAsync().ConfigureAwait(false)).Count == 0)
            {
                return false;
            }

            if (await GetDropDelayAsync().ConfigureAwait(false) > delayMs)
            {
                delayMs = (int) (await GetDropDelayAsync().ConfigureAwait(false));
            }

            beforeMoveCount = (await _objectSearchService.GetFindedListAsync().ConfigureAwait(false)).Count;
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
                var id = (await _objectSearchService.GetFindedListAsync().ConfigureAwait(false))[i];
                await MoveItemAsync(id, 0, moveIntoId, x, y, z).ConfigureAwait(false);
                Thread.Sleep(delayMs);
            }

            await _objectSearchService.FindTypeExAsync(itemsType, itemsColor, container, false).ConfigureAwait(false);
            return (await _objectSearchService.GetFindedListAsync().ConfigureAwait(false)).Count == beforeMoveCount - moveItemsCount;
        }

        public Task SetCatchBagAsync(uint objectId)
        {
            return Client.SendPacketAsync(PacketType.SCSetCatchBag,
                objectId);
        }

        public Task UnsetCatchBagAsync()
        {
            return Client.SendPacketAsync(PacketType.SCUnsetCatchBag);
        }
    }
}