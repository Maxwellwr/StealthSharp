#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LayerService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class LayerService : BaseService, ILayerService
    {
        private readonly ICharStatsService _charStatsService;
        private readonly IObjectSearchService _objectSearchService;
        private readonly IMoveItemService _moveItemService;

        public LayerService(
            IStealthSharpClient<ushort, uint, ushort> client,
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

            var objId = await ObjAtLayerExAsync(Layer.LHand, await _charStatsService.GetSelfAsync());
            if (objId != 0)
            {
                lh = await _moveItemService.MoveItemAsync(objId, 1, await _objectSearchService.GetBackpackAsync(), 0xff,
                    0xff, 0);
            }

            objId = await ObjAtLayerExAsync(Layer.RHand, await _charStatsService.GetSelfAsync());
            if (objId != 0)
            {
                rh = await _moveItemService.MoveItemAsync(objId, 1, await _objectSearchService.GetBackpackAsync(), 0xff,
                    0xff, 0);
            }

            return lh && rh;
        }

        public Task<bool> DressSavedSetAsync()
        {
            return EquipDressSetAsync();
        }

        public async Task<bool> EquipAsync(Layer layer, uint objId)
        {
            if (!System.Enum.IsDefined(typeof(Layer), layer))
            {
                return false;
            }

            if (!await _moveItemService.DragItemAsync(objId, 1))
            {
                return false;
            }

            Thread.Sleep(20);

            return await WearItemAsync(layer, objId);
        }

        public async Task<bool> EquipDressSetAsync()
        {
            var result = true;
            var clientVersion = await Client.SendPacketAsync<int>(PacketType.SCGetClientVersionInt);
            if (clientVersion < 7007400)
            {
                var delay = await GetDressSpeedAsync();
                var data = await Client.SendPacketAsync<List<LayerObject>>(PacketType.SCGetDressSet);
                foreach (var item in data)
                {
                    result &= await EquipAsync(item.Layer, item.ItemId);
                    Thread.Sleep(delay * 1000);
                }
            }
            else
            {
                await Client.SendPacketAsync(PacketType.SCEquipItemsSetMacro);
            }

            return result;
        }

        public async Task<bool> EquiptAsync(Layer layer, ushort objType)
        {
            var obj = await _objectSearchService.FindTypeAsync(objType, await _objectSearchService.GetBackpackAsync());
            if (obj == 0 || !System.Enum.IsDefined(typeof(Layer), layer))
            {
                return false;
            }

            if (!await _moveItemService.DragItemAsync(obj, 1))
            {
                return false;
            }

            return await WearItemAsync(layer, obj);
        }

        public Task<Layer> GetLayerAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, Layer>(PacketType.SCGetLayer, objId);
        }

        public async Task<uint> ObjAtLayerAsync(Layer layer)
        {
            return await ObjAtLayerExAsync(layer, await _charStatsService.GetSelfAsync());
        }

        public Task<uint> ObjAtLayerExAsync(Layer layer, uint playerId)
        {
            if (!System.Enum.IsDefined(typeof(Layer), layer))
            {
                return Task.FromResult(0u);
            }

            return Client.SendPacketAsync<(Layer, uint), uint>(PacketType.SCObjAtLayerEx, (layer, playerId));
        }

        public Task SetDressAsync()
        {
            return Client.SendPacketAsync(PacketType.SCSetDress);
        }

        public async Task<bool> UndressAsync()
        {
            var result = true;
            var clientVersion = await Client.SendPacketAsync<int>(PacketType.SCGetClientVersionInt);
            if (clientVersion < 7007400)
            {
                foreach (Layer layer in System.Enum.GetValues(typeof(Layer)))
                {
                    if (await ObjAtLayerExAsync(layer, await _charStatsService.GetSelfAsync()) > 0)
                    {
                        result &= await UnequipAsync(layer);
                        Thread.Sleep(await GetDressSpeedAsync() * 1000);
                    }
                }
            }
            else
            {
                await Client.SendPacketAsync(PacketType.SCUnequipItemsSetMacro);
            }

            return result;
        }

        public async Task<bool> UnequipAsync(Layer layer)
        {
            if (!System.Enum.IsDefined(typeof(Layer), layer))
            {
                return false;
            }

            var objId = await ObjAtLayerExAsync(layer, await _charStatsService.GetSelfAsync());
            if (objId != 0)
            {
                return await _moveItemService.MoveItemAsync(objId, 1, await _objectSearchService.GetBackpackAsync(), 0,
                    0, 0);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> WearItemAsync(Layer layer, uint objId)
        {
            if (await _moveItemService.GetPickedUpItemAsync() == 0)
            {
                return false;
            }

            if (!System.Enum.IsDefined(typeof(Layer), layer))
            {
                return false;
            }

            if (await _charStatsService.GetSelfAsync() == 0x00)
            {
                return false;
            }

            await Client.SendPacketAsync(PacketType.SCWearItem, (layer, objId));
            await _moveItemService.SetPickedUpItemAsync(0);
            Thread.Sleep(1000);
            var objAtLayer = await ObjAtLayerAsync(layer);
            return objAtLayer == objId;
        }
    }
}