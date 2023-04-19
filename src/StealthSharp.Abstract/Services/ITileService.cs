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
        Task<string> ConvertIntToFlagsAsync(byte group, uint flags);

        Task<LandTileData> GetLandTileDataAsync(ushort tile);

        Task<List<FoundTile>> GetLandTilesArrayAsync(ushort xmin, ushort ymin, ushort xmax, ushort ymax, byte worldNum,
            ushort tileType);

        Task<List<FoundTile>> GetLandTilesArrayExAsync(ushort xmin, ushort ymin, ushort xmax, ushort ymax, byte worldNum,
            ushort[] tileTypes);

        Task<byte> GetLayerCountAsync(ushort x, ushort y, byte worldNum);

        Task<MapCell> GetMapCellAsync(ushort x, ushort y, byte worldNum);

        Task<sbyte> GetNextStepZAsync(ushort currX, ushort currY, ushort destX, ushort destY, byte worldNum, sbyte z);

        Task<StaticTileData> GetStaticTileDataAsync(ushort tile);

        Task<List<FoundTile>> GetStaticTilesArrayAsync(ushort xmin, ushort ymin, ushort xmax, ushort ymax, byte worldNum,
            ushort tileType);

        Task<List<FoundTile>> GetStaticTilesArrayExAsync(ushort xmin, ushort ymin, ushort xmax, ushort ymax, byte worldNum,
            ushort[] tileTypes);

        Task<byte> GetSurfaceZAsync(ushort x, ushort y, byte worldNum);

        Task<uint> GetTileFlagsAsync(TileFlagsType group, ushort tile);

        Task<(bool result, sbyte destZ)> IsWorldCellPassableAsync(ushort currX, ushort currY, sbyte z, ushort destX, ushort destY,
            byte worldNum);

        Task<List<StaticItemRealXY>> ReadStaticsXYAsync(ushort x, ushort y, byte worldNum);
    }
}