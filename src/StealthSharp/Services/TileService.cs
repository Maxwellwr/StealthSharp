#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TileService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class TileService : BaseService, ITileService
    {
        public TileService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<LandTileData> GetLandTileDataAsync(ushort tile)
        {
            return Client.SendPacketAsync<ushort, LandTileData>(PacketType.SCGetLandTileData, tile);
        }

        public Task<List<FoundTile>> GetLandTilesArrayAsync(WorldRect rect, byte worldNum, ushort tileType)
        {
            return Client.SendPacketAsync<(ushort, ushort, ushort, ushort, byte, ushort), List<FoundTile>>(
                PacketType.SCGetLandTilesArray,
                (rect.XMin, rect.YMin, rect.XMax, rect.YMax, worldNum, tileType));
        }

        public async Task<List<FoundTile>> GetLandTilesArrayExAsync(WorldRect rect, byte worldNum, ushort[] tileTypes)
        {
            var tasks = tileTypes.Select(t => GetLandTilesArrayAsync(rect, worldNum, t));
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).SelectMany(t => t).Distinct(new FoundTileComparer()).ToList();
        }

        public Task<byte> GetLayerCountAsync(WorldPoint point, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), byte>(PacketType.SCGetLayerCount, (point.X, point.Y, worldNum));
        }

        public Task<MapCell> GetMapCellAsync(WorldPoint point, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), MapCell>(PacketType.SCGetCell, (point.X, point.Y, worldNum));
        }

        public Task<sbyte> GetNextStepZAsync(WorldPoint current, WorldPoint dest, byte worldNum, sbyte z)
        {
            return Client.SendPacketAsync<(ushort, ushort, ushort, ushort, byte, sbyte), sbyte>(
                PacketType.SCGetNextStepZ, (current.X, current.Y, dest.X, dest.Y, worldNum, z));
        }

        public Task<StaticTileData> GetStaticTileDataAsync(ushort tile)
        {
            return Client.SendPacketAsync<ushort, StaticTileData>(PacketType.SCGetStaticTileData, tile);
        }

        public Task<List<FoundTile>> GetStaticTilesArrayAsync(WorldRect rect,
            byte worldNum,
            ushort tileType)
        {
            return Client.SendPacketAsync<(ushort, ushort, ushort, ushort, byte, ushort), List<FoundTile>>(
                PacketType.SCGetStaticTilesArray,
                (rect.XMin, rect.YMin, rect.XMax, rect.YMax, worldNum, tileType));
        }

        public async Task<List<FoundTile>> GetStaticTilesArrayExAsync(WorldRect rect, byte worldNum, ushort[] tileTypes)
        {
            var tasks = tileTypes.Select(t => GetStaticTilesArrayAsync(rect, worldNum, t));
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).SelectMany(t => t).Distinct(new FoundTileComparer()).ToList();
        }

        public Task<byte> GetSurfaceZAsync(WorldPoint point, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), byte>(PacketType.SCGetSurfaceZ, (point.X, point.Y, worldNum));
        }

        public Task<(bool result, sbyte destZ)> IsWorldCellPassableAsync(WorldPoint3D current, WorldPoint dest, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, sbyte, ushort, ushort, byte), (bool, sbyte)>(
                PacketType.SCIsWorldCellPassable,
                (current.X, current.Y, current.Z, dest.X, dest.Y, worldNum));
        }

        public Task<List<StaticItemRealXY>> ReadStaticsXYAsync(WorldPoint point, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, byte), List<StaticItemRealXY>>(PacketType.SCReadStaticsXY,
                (point.X, point.Y, worldNum));
        }
    }
}