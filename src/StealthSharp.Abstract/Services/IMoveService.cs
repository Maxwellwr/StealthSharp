#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMoveService.cs" company="StealthSharp">
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
    public interface IMoveService
    {
        Task SetMoveBetweenTwoCornersAsync(bool value);
        Task<bool> GetMoveBetweenTwoCornersAsync();
        Task SetRunMountTimerAsync(ushort value);
        Task<ushort> GetRunMountTimerAsync();
        Task SetRunUnMountTimerAsync(ushort value);
        Task<ushort> GetRunUnMountTimerAsync();
        Task SetWalkMountTimerAsync(ushort value);
        Task<ushort> GetWalkMountTimerAsync();
        Task SetWalkUnmountTimerAsync(ushort value);
        Task<ushort> GetWalkUnmountTimerAsync();
        Task SetMoveCheckStaminaAsync(ushort value);
        Task<ushort> GetMoveCheckStaminaAsync();
        Task SetMoveHeuristicMultAsync(int value);
        Task<int> GetMoveHeuristicMultAsync();
        Task SetMoveOpenDoorAsync(bool value);
        Task<bool> GetMoveOpenDoorAsync();
        Task SetMoveThroughCornerAsync(bool value);
        Task<bool> GetMoveThroughCornerAsync();
        Task SetMoveThroughNPCAsync(ushort value);
        Task<ushort> GetMoveThroughNPCAsync();
        Task SetMoveTurnCostAsync(int value);
        Task<int> GetMoveTurnCostAsync();
        Task<byte> GetPredictedDirectionAsync();
        Task<ushort> GetPredictedXAsync();
        Task<ushort> GetPredictedYAsync();
        Task<sbyte> GetPredictedZAsync();
        WorldPoint CalcCoord(WorldPoint point, Direction dir);
        Direction CalcDir(WorldPoint from, WorldPoint to);
        Task ClearBadLocationListAsync();
        Task ClearBadObjectListAsync();
        ushort Dist(WorldPoint source, WorldPoint dest);
        Task<List<WorldPoint3D>> GetPathArrayAsync(WorldPoint dest, bool optimized, int accuracy);

        Task<List<WorldPoint3D>> GetPathArray3DAsync(PathReqeust pathReqeust);

        Task<uint> GetLastStepQUsedDoorAsync();
        Task<bool> MoveXYAsync(WorldPoint dest, bool optimized, int accuracy, bool running);
        Task<bool> MoveXYZAsync(WorldPoint3D dest, int accuracyXY, int accuracyZ, bool running);
        Task<bool> NewMoveXYAsync(WorldPoint dest, bool optimized, int accuracy, bool running);
        Task StopMoverAsync();
        Task OpenDoorAsync();
        Task<bool> RawMoveAsync(byte direction, bool running);
        Task SetBadLocationAsync(WorldPoint point);
        Task SetBadObjectAsync(ushort objType, ushort color, byte radius);
        Task SetGoodLocationAsync(WorldPoint point);
        Task<byte> StepAsync(byte direction, bool running);
        Task<int> StepQAsync(byte direction, bool running);
    }
}