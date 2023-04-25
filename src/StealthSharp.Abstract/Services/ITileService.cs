#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ITileService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface ITileService
    {
        Task<LandTileData> GetLandTileDataAsync(ushort tile);

        Task<List<FoundTile>> GetLandTilesArrayAsync(WorldRect rect, byte worldNum, ushort tileType);

        Task<List<FoundTile>> GetLandTilesArrayExAsync(WorldRect rect, byte worldNum, ushort[] tileTypes);

        Task<byte> GetLayerCountAsync(WorldPoint point, byte worldNum);

        Task<MapCell> GetMapCellAsync(WorldPoint point, byte worldNum);

        Task<sbyte> GetNextStepZAsync(WorldPoint current, WorldPoint destination, byte worldNum, sbyte z);

        Task<StaticTileData> GetStaticTileDataAsync(ushort tile);

        Task<List<FoundTile>> GetStaticTilesArrayAsync(WorldRect rect, byte worldNum, ushort tileType);

        Task<List<FoundTile>> GetStaticTilesArrayExAsync(WorldRect rect, byte worldNum, ushort[] tileTypes);

        Task<byte> GetSurfaceZAsync(WorldPoint point, byte worldNum);

        Task<(bool result, sbyte destZ)> IsWorldCellPassableAsync(WorldPoint3D current, WorldPoint dest, byte worldNum);

        Task<List<StaticItemRealXY>> ReadStaticsXYAsync(WorldPoint point, byte worldNum);
    }
}