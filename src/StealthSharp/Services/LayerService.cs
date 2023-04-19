#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LayerService.cs" company="StealthSharp">
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
    public class LayerService : BaseService, ILayerService
    {
        private readonly ICharStatsService _charStatsService;
        private readonly IObjectSearchService _objectSearchService;
        private readonly IMoveItemService _moveItemService;

        public LayerService(
            IStealthSharpClient client,
            ICharStatsService charStatsService,
            IMoveItemService moveItemService,
            IObjectSearchService objectSearchService)
            : base(client)
        {
            _charStatsService = charStatsService;
            _objectSearchService = objectSearchService;
            _moveItemService = moveItemService;
        }

        public Task SetDressSpeedAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetDressSpeed, value);
        }

        public Task<ushort> GetDressSpeedAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetDressSpeed);
        }

        public async Task<bool> DisarmAsync()
        {
            var lh = true;
            var rh = true;

            var objId = await ObjAtLayerExAsync(Layer.LHand, await _charStatsService.GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);
            if (objId != 0)
                lh = await _moveItemService.MoveItemAsync(objId, 1, await _objectSearchService.GetBackpackAsync().ConfigureAwait(false), 0xff,
                    0xff, 0).ConfigureAwait(false);

            objId = await ObjAtLayerExAsync(Layer.RHand, await _charStatsService.GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);
            if (objId != 0)
                rh = await _moveItemService.MoveItemAsync(objId, 1, await _objectSearchService.GetBackpackAsync().ConfigureAwait(false), 0xff,
                    0xff, 0).ConfigureAwait(false);

            return lh && rh;
        }

        public Task<bool> DressSavedSetAsync()
        {
            return EquipDressSetAsync();
        }

        public async Task<bool> EquipAsync(Layer layer, uint objId)
        {
            if (!Enum.IsDefined(typeof(Layer), layer)) return false;

            if (!await _moveItemService.DragItemAsync(objId, 1).ConfigureAwait(false)) return false;

            Thread.Sleep(20);

            return await WearItemAsync(layer, objId).ConfigureAwait(false);
        }

        public async Task<bool> EquipDressSetAsync()
        {
            var result = true;
            var clientVersion = await Client.SendPacketAsync<int>(PacketType.SCGetClientVersionInt).ConfigureAwait(false);
            if (clientVersion < 7007400)
            {
                var delay = await GetDressSpeedAsync().ConfigureAwait(false);
                var data = await Client.SendPacketAsync<List<LayerObject>>(PacketType.SCGetDressSet).ConfigureAwait(false);
                foreach (var item in data)
                {
                    result &= await EquipAsync(item.Layer, item.ItemId).ConfigureAwait(false);
                    Thread.Sleep(delay * 1000);
                }
            }
            else
            {
                await Client.SendPacketAsync(PacketType.SCEquipItemsSetMacro).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<bool> EquiptAsync(Layer layer, ushort objType)
        {
            var obj = await _objectSearchService.FindTypeAsync(objType, await _objectSearchService.GetBackpackAsync().ConfigureAwait(false)).ConfigureAwait(false);
            if (obj == 0 || !Enum.IsDefined(typeof(Layer), layer)) return false;

            if (!await _moveItemService.DragItemAsync(obj, 1).ConfigureAwait(false)) return false;

            return await WearItemAsync(layer, obj).ConfigureAwait(false);
        }

        public Task<Layer> GetLayerAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, Layer>(PacketType.SCGetLayer, objId);
        }

        public async Task<uint> ObjAtLayerAsync(Layer layer)
        {
            return await ObjAtLayerExAsync(layer, await _charStatsService.GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);
        }

        public Task<uint> ObjAtLayerExAsync(Layer layer, uint playerId)
        {
            if (!Enum.IsDefined(typeof(Layer), layer)) return Task.FromResult(0u);

            return Client.SendPacketAsync<(Layer, uint), uint>(PacketType.SCObjAtLayerEx, (layer, playerId));
        }

        public Task SetDressAsync()
        {
            return Client.SendPacketAsync(PacketType.SCSetDress);
        }

        public async Task<bool> UndressAsync()
        {
            var result = true;
            var clientVersion = await Client.SendPacketAsync<int>(PacketType.SCGetClientVersionInt).ConfigureAwait(false);
            if (clientVersion < 7007400)
            {
                foreach (Layer layer in Enum.GetValues(typeof(Layer)))
                    if (await ObjAtLayerExAsync(layer, await _charStatsService.GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false) > 0)
                    {
                        result &= await UnequipAsync(layer).ConfigureAwait(false);
                        Thread.Sleep(await GetDressSpeedAsync().ConfigureAwait(false) * 1000);
                    }
            }
            else
            {
                await Client.SendPacketAsync(PacketType.SCUnequipItemsSetMacro).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<bool> UnequipAsync(Layer layer)
        {
            if (!Enum.IsDefined(typeof(Layer), layer)) return false;

            var objId = await ObjAtLayerExAsync(layer, await _charStatsService.GetSelfAsync().ConfigureAwait(false)).ConfigureAwait(false);
            if (objId != 0)
                return await _moveItemService.MoveItemAsync(objId, 1, await _objectSearchService.GetBackpackAsync().ConfigureAwait(false), 0,
                    0, 0).ConfigureAwait(false);

            return false;
        }

        public async Task<bool> WearItemAsync(Layer layer, uint objId)
        {
            if (await _moveItemService.GetPickedUpItemAsync().ConfigureAwait(false) == 0) return false;

            if (!Enum.IsDefined(typeof(Layer), layer)) return false;

            if (await _charStatsService.GetSelfAsync().ConfigureAwait(false) == 0x00) return false;

            await Client.SendPacketAsync(PacketType.SCWearItem, (layer, objId)).ConfigureAwait(false);
            await _moveItemService.SetPickedUpItemAsync(0).ConfigureAwait(false);
            Thread.Sleep(1000);
            var objAtLayer = await ObjAtLayerAsync(layer).ConfigureAwait(false);
            return objAtLayer == objId;
        }
    }
}