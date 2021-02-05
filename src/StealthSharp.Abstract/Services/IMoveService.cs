#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMoveService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;

namespace StealthSharp.Services
{
    public interface IMoveService
    {
        void SetMoveBetweenTwoCorners(bool value);
        Task<bool> GetMoveBetweenTwoCornersAsync();
        void SetRunMountTimer(ushort value);
        Task<ushort> GetRunMountTimerAsync();
        void SetRunUnMountTimer(ushort value);
        Task<ushort> GetRunUnMountTimerAsync();
        void SetWalkMountTimer(ushort value);
        Task<ushort> GetWalkMountTimerAsync();
        void SetWalkUnmountTimer(ushort value);
        Task<ushort> GetWalkUnmountTimerAsync();
        void SetMoveCheckStamina(ushort value);
        Task<ushort> GetMoveCheckStaminaAsync();
        void SetMoveHeuristicMult(int value);
        Task<int> GetMoveHeuristicMultAsync();
        void SetMoveOpenDoor(bool value);
        Task<bool> GetMoveOpenDoorAsync();
        void SetMoveThroughCorner(bool value);
        Task<bool> GetMoveThroughCornerAsync();
        void SetMoveThroughNPC(ushort value);
        Task<ushort> GetMoveThroughNPCAsync();
        void SetMoveTurnCost(int value);
        Task<int> GetMoveTurnCostAsync();
        Task<byte> GetPredictedDirectionAsync();
        Task<ushort> GetPredictedXAsync();
        Task<ushort> GetPredictedYAsync();
        Task<sbyte> GetPredictedZAsync();
        ( ushort x2, ushort y2) CalcCoord(ushort x, ushort y, Direction dir);
        Direction CalcDir(ushort xFrom, ushort yFrom, ushort xTo, ushort yTo);
        void ClearBadLocationList();
        void ClearBadObjectList();
        ushort Dist(ushort x1, ushort y1, ushort x2, ushort y2);
        Task<List<MyPoint>> GetPathArrayAsync(ushort destX, ushort destY, bool optimized, int accuracy);

        Task<List<MyPoint>> GetPathArray3DAsync(PathReqeust pathReqeust);

        Task<uint> GetLastStepQUsedDoorAsync();
        Task<bool> MoveXYAsync(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running);
        Task<bool> MoveXYZAsync(ushort xDst, ushort yDst, sbyte zDst, int accuracyXY, int accuracyZ, bool running);
        Task<bool> NewMoveXYAsync(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running);
        void StopMover();
        void OpenDoor();
        Task<bool> RawMoveAsync(byte direction, bool running);
        void SetBadLocation(ushort x, ushort y);
        void SetBadObject(ushort objType, ushort color, byte radius);
        void SetGoodLocation(ushort x, ushort y);
        Task<byte> StepAsync(byte direction, bool running);
        Task<int> StepQAsync(byte direction, bool running);
    }
}