#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MoveService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class MoveService : BaseService, IMoveService
    {
        public MoveService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task SetMoveBetweenTwoCornersAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveBetweenTwoCorners, value);
        }

        public Task<bool> GetMoveBetweenTwoCornersAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetMoveBetweenTwoCorners);
        }

        public Task SetRunMountTimerAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetRunMountTimer, value);
        }

        public Task<ushort> GetRunMountTimerAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetRunMountTimer);
        }

        public Task SetRunUnMountTimerAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetRunUnmountTimer, value);
        }

        public Task<ushort> GetRunUnMountTimerAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetRunUnmountTimer);
        }

        public Task SetWalkMountTimerAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetWalkMountTimer, value);
        }

        public Task<ushort> GetWalkMountTimerAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetWalkMountTimer);
        }

        public Task SetWalkUnmountTimerAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetWalkUnmountTimer, value);
        }

        public Task<ushort> GetWalkUnmountTimerAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetWalkUnmountTimer);
        }

        public Task SetMoveCheckStaminaAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveCheckStamina, value);
        }

        public Task<ushort> GetMoveCheckStaminaAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetMoveCheckStamina);
        }

        public Task SetMoveHeuristicMultAsync(int value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveHeuristicMult, value);
        }

        public Task<int> GetMoveHeuristicMultAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetMoveHeuristicMult);
        }

        public Task SetMoveOpenDoorAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveOpenDoor, value);
        }

        public Task<bool> GetMoveOpenDoorAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetMoveOpenDoor);
        }

        public Task SetMoveThroughCornerAsync(bool value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveThroughCorner, value);
        }

        public Task<bool> GetMoveThroughCornerAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetMoveThroughCorner);
        }

        public Task SetMoveThroughNPCAsync(ushort value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveThroughNPC, value);
        }

        public Task<ushort> GetMoveThroughNPCAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetMoveThroughNPC);
        }

        public Task SetMoveTurnCostAsync(int value)
        {
            return Client.SendPacketAsync(PacketType.SCSetMoveTurnCost, value);
        }

        public Task<int> GetMoveTurnCostAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetMoveTurnCost);
        }

        public Task<byte> GetPredictedDirectionAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCPredictedDirection);
        }

        public Task<ushort> GetPredictedXAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCPredictedX);
        }

        public Task<ushort> GetPredictedYAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCPredictedY);
        }

        public Task<sbyte> GetPredictedZAsync()
        {
            return Client.SendPacketAsync<sbyte>(PacketType.SCPredictedZ);
        }

        public WorldPoint CalcCoord(WorldPoint point, Direction dir)
        {
            var x2 = point.X;
            var y2 = point.Y;

            if (dir == Direction.NorthEast ||
                dir == Direction.East ||
                dir == Direction.SouthEast)
                x2 = (ushort)(point.X + 1);

            if (dir == Direction.SouthWest ||
                dir == Direction.West ||
                dir == Direction.NorthWest)
                x2 = (ushort)(point.X - 1);

            if (dir == Direction.North ||
                dir == Direction.South)
                x2 = point.X;

            if (dir == Direction.SouthEast ||
                dir == Direction.South ||
                dir == Direction.SouthWest)
                y2 = (ushort)(point.Y + 1);

            if (dir == Direction.NorthWest ||
                dir == Direction.North ||
                dir == Direction.NorthEast)
                y2 = (ushort)(point.Y - 1);

            if (dir == Direction.East ||
                dir == Direction.West)
                y2 = point.Y;

            return new WorldPoint(x2, y2);
        }

        public Direction CalcDir(WorldPoint from, WorldPoint to)
        {
            var diffX = (ushort)Math.Abs(from.X - to.X);
            var diffY = (ushort)Math.Abs(from.Y - to.Y);
            if (diffX == 0 && diffY == 0) return Direction.Unknown;
            if (diffX / (diffY + 0.1) >= 2) return from.X > to.X ? Direction.West : Direction.East;
            if (diffY / (diffX + 0.1) >= 2) return from.Y > to.Y ? Direction.North : Direction.South;
            if (from.X > to.X && from.Y > to.Y) return Direction.NorthWest;
            if (from.X > to.X && from.Y < to.Y) return Direction.SouthWest;
            if (from.X < to.X && from.Y > to.Y) return Direction.NorthEast;
            if (from.X < to.X && from.Y < to.Y) return Direction.SouthEast;

            return Direction.Unknown;
        }

        public Task ClearBadLocationListAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClearBadLocationList);
        }

        public Task ClearBadObjectListAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClearBadObjectList);
        }

        public ushort Dist(WorldPoint source, WorldPoint dest)
        {
            var dx = (ushort)Math.Abs(source.X - dest.X);
            var dy = (ushort)Math.Abs(source.Y - dest.Y);

            var ret = dx > dy ? dy : dx;
            var my = (ushort)Math.Abs(dx - dy);
            return (ushort)(ret + my);
        }

        public Task<List<WorldPoint3D>> GetPathArrayAsync(WorldPoint dest, bool optimized, int accuracy)
        {
            return Client.SendPacketAsync<(ushort, ushort, bool, int), List<WorldPoint3D>>(PacketType.SCGetPathArray,
                (dest.X, dest.Y, optimized, accuracy));
        }

        public Task<List<WorldPoint3D>> GetPathArray3DAsync(PathReqeust pathRequest)
        {
            return Client.SendPacketAsync<PathReqeust, List<WorldPoint3D>>(PacketType.SCGetPathArray3D, pathRequest);
        }

        public Task<uint> GetLastStepQUsedDoorAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastStepQUsedDoor);
        }

        public Task<bool> MoveXYAsync(WorldPoint dest, bool optimized, int accuracy, bool running)
        {
            return Client.SendPacketAsync<(ushort, ushort, bool, int, bool), bool>(PacketType.SCMoveXY,
                (dest.X, dest.Y, optimized, accuracy, running));
        }

        public Task<bool> MoveXYZAsync(WorldPoint3D dest, int accuracyXY, int accuracyZ, bool running)
        {
            return Client.SendPacketAsync<(ushort, ushort, sbyte, int, int, bool), bool>(PacketType.SCMoveXYZ,
                (dest.X, dest.Y, dest.Z, accuracyXY, accuracyZ, running));
        }

        public Task<bool> NewMoveXYAsync(WorldPoint dest, bool optimized, int accuracy, bool running)
        {
            return MoveXYZAsync(new WorldPoint3D(dest.X, dest.Y, 0), accuracy, 255, running);
        }

        public Task StopMoverAsync()
        {
            return Client.SendPacketAsync(PacketType.SCMoverStop);
        }

        public Task OpenDoorAsync()
        {
            return Client.SendPacketAsync(PacketType.SCOpenDoor);
        }

        public Task<bool> RawMoveAsync(byte direction, bool running)
        {
            throw new NotImplementedException();
        }

        public Task SetBadLocationAsync(WorldPoint point)
        {
            return Client.SendPacketAsync(PacketType.SCSetBadLocation, point);
        }

        public Task SetBadObjectAsync(ushort objType, ushort color, byte radius)
        {
            return Client.SendPacketAsync(PacketType.SCSetBadObject, (objType, color, radius));
        }

        public Task SetGoodLocationAsync(WorldPoint point)
        {
            return Client.SendPacketAsync(PacketType.SCSetGoodLocation, point);
        }

        public Task<byte> StepAsync(byte direction, bool running)
        {
            return Client.SendPacketAsync<(byte, bool), byte>(PacketType.SCStep, (direction, running));
        }

        public Task<int> StepQAsync(byte direction, bool running)
        {
            return Client.SendPacketAsync<(byte, bool), int>(PacketType.SCStepQ, (direction, running));
        }
    }
}