#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TileService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class TileService : BaseService, ITileService
    {
        public TileService(IStealthSharpClient client)
            : base(client)
        {
        }

// TODO: выяснить что что это за метод
        public Task<string> ConvertIntToFlagsAsync(byte group, uint flags)
        {
            return Client.SendPacketAsync<(byte, uint), string>(PacketType.SCConvertIntegerToFlags, (group, flags));
        }

        public Task<LandTileData> GetLandTileDataAsync(ushort tile)
        {
            return Client.SendPacketAsync<ushort, LandTileData>(PacketType.SCGetLandTileData, tile);
        }

        public Task<List<FoundTile>> GetLandTilesArrayAsync(ushort xMin, ushort yMin, ushort xMax, ushort yMax,
            byte worldNum,
            ushort tileType)
        {
            return Client.SendPacketAsync<(ushort, ushort, ushort, ushort, byte, ushort), List<FoundTile>>(
                PacketType.SCGetLandTilesArray,
                (xMin, yMin, xMax, yMax, worldNum, tileType));
        }

        public async Task<List<FoundTile>> GetLandTilesArrayExAsync(ushort xmin, ushort ymin, ushort xmax, ushort ymax,
            byte worldNum,
            ushort[] tileTypes)
        {
            var tasks = tileTypes.Select(t => GetLandTilesArrayAsync(xmin, ymin, xmax, ymax, worldNum, t));
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).SelectMany(t => t).Distinct(new FoundTileComparer()).ToList();
        }

        public Task<byte> GetLayerCountAsync(ushort x, ushort y, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), byte>(PacketType.SCGetLayerCount, (x, y, worldNum));
        }

        public Task<MapCell> GetMapCellAsync(ushort x, ushort y, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), MapCell>(PacketType.SCGetCell, (x, y, worldNum));
        }

        public Task<sbyte> GetNextStepZAsync(ushort currX, ushort currY, ushort destX, ushort destY, byte worldNum,
            sbyte z)
        {
            return Client.SendPacketAsync<(ushort, ushort, ushort, ushort, byte, sbyte), sbyte>(
                PacketType.SCGetNextStepZ, (currX, currY, destX, destY, worldNum, z));
        }

        public Task<StaticTileData> GetStaticTileDataAsync(ushort tile)
        {
            return Client.SendPacketAsync<ushort, StaticTileData>(PacketType.SCGetStaticTileData, tile);
        }

        public Task<List<FoundTile>> GetStaticTilesArrayAsync(ushort xMin, ushort yMin, ushort xMax, ushort yMax,
            byte worldNum,
            ushort tileType)
        {
            return Client.SendPacketAsync<(ushort, ushort, ushort, ushort, byte, ushort), List<FoundTile>>(
                PacketType.SCGetStaticTilesArray,
                (xMin, yMin, xMax, yMax, worldNum, tileType));
        }

        public async Task<List<FoundTile>> GetStaticTilesArrayExAsync(ushort xmin, ushort ymin, ushort xmax,
            ushort ymax, byte worldNum, ushort[] tileTypes)
        {
            var tasks = tileTypes.Select(t => GetStaticTilesArrayAsync(xmin, ymin, xmax, ymax, worldNum, t));
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).SelectMany(t => t).Distinct(new FoundTileComparer()).ToList();
        }

        public Task<byte> GetSurfaceZAsync(ushort x, ushort y, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), byte>(PacketType.SCGetSurfaceZ, (x, y, worldNum));
        }

        public Task<uint> GetTileFlagsAsync(TileFlagsType group, ushort tile)
        {
            return Client.SendPacketAsync<(TileFlagsType, ushort), uint>(PacketType.SCGetTileFlags, (group, tile));
        }

        public Task<(bool result, sbyte destZ)> IsWorldCellPassableAsync(ushort currX, ushort currY, sbyte currZ,
            ushort destX, ushort destY, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, sbyte, ushort, ushort, byte), (bool, sbyte)>(
                PacketType.SCIsWorldCellPassable,
                (currX, currY, currZ, destX, destY, worldNum));
        }

        public Task<List<StaticItemRealXY>> ReadStaticsXYAsync(ushort x, ushort y, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), List<StaticItemRealXY>>(PacketType.SCReadStaticsXY,
                (x, y, worldNum));
        }
    }
}