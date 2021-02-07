#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MoveService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class MoveService : BaseService, IMoveService
    {
        public MoveService(IStealthSharpClient<ushort, uint, ushort> client)
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

        public ( ushort x2, ushort y2) CalcCoord(ushort x, ushort y, Direction dir)
        {
            var x2 = x;
            var y2 = y;

            if ((dir == Direction.NorthEast) ||
                (dir == Direction.East) ||
                (dir == Direction.SouthEast))
            {
                x2 = (ushort) (x + 1);
            }

            if ((dir == Direction.SouthWest) ||
                (dir == Direction.West) ||
                (dir == Direction.NorthWest))
            {
                x2 = (ushort) (x - 1);
            }

            if ((dir == Direction.North) ||
                (dir == Direction.South))
            {
                x2 = x;
            }

            if ((dir == Direction.SouthEast) ||
                (dir == Direction.South) ||
                (dir == Direction.SouthWest))
            {
                y2 = (ushort) (y + 1);
            }

            if ((dir == Direction.NorthWest) ||
                (dir == Direction.North) ||
                (dir == Direction.NorthEast))
            {
                y2 = (ushort) (y - 1);
            }

            if ((dir == Direction.East) ||
                (dir == Direction.West))
            {
                y2 = y;
            }

            return (x2, y2);
        }

        public Direction CalcDir(ushort xFrom, ushort yFrom, ushort xTo, ushort yTo)
        {
            var diffx = (ushort) Math.Abs(xFrom - xTo);
            var diffy = (ushort) Math.Abs(yFrom - yTo);
            if (diffx == 0 && diffy == 0)
            {
                return Direction.Unknown;
            }

            if ((diffx / (diffy + 0.1)) >= 2)
            {
                if (xFrom > xTo)
                {
                    return Direction.West;
                }

                return Direction.East;
            }

            if ((diffy / (diffx + 0.1)) >= 2)
            {
                if (yFrom > yTo)
                {
                    return Direction.North;
                }

                return Direction.South;
            }

            if (xFrom > xTo && yFrom > yTo)
            {
                return Direction.NorthWest;
            }

            if (xFrom > xTo && yFrom < yTo)
            {
                return Direction.SouthWest;
            }

            if (xFrom < xTo && yFrom > yTo)
            {
                return Direction.NorthEast;
            }

            if (xFrom < xTo && yFrom < yTo)
            {
                return Direction.SouthEast;
            }

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

        public ushort Dist(ushort x1, ushort y1, ushort x2, ushort y2)
        {
            var dx = (ushort) Math.Abs(x1 - x2);
            var dy = (ushort) Math.Abs(y1 - y2);

            var ret = (dx > dy) ? dy : dx;
            var my = (ushort) Math.Abs(dx - dy);
            return (ushort) (ret + my);
        }

        public Task<List<MyPoint>> GetPathArrayAsync(ushort destX, ushort destY, bool optimized, int accuracy)
        {
            return Client.SendPacketAsync<(ushort, ushort, bool, int), List<MyPoint>>(PacketType.SCGetPathArray,
                (destX, destY, optimized, accuracy));
        }

        public Task<List<MyPoint>> GetPathArray3DAsync(PathReqeust pathRequest)
        {
            return Client.SendPacketAsync<PathReqeust, List<MyPoint>>(PacketType.SCGetPathArray3D, pathRequest);
        }

        public Task<uint> GetLastStepQUsedDoorAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastStepQUsedDoor);
        }

        public Task<bool> MoveXYAsync(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running)
        {
            return Client.SendPacketAsync<(ushort, ushort, bool, int, bool), bool>(PacketType.SCMoveXY,
                (xDst, yDst, optimized, accuracy, running));
        }

        public Task<bool> MoveXYZAsync(ushort xDst, ushort yDst, sbyte zDst, int accuracyXY, int accuracyZ,
            bool running)
        {
            return Client.SendPacketAsync<(ushort, ushort, sbyte, int, int, bool), bool>(PacketType.SCMoveXYZ,
                (xDst, yDst, zDst, accuracyXY, accuracyZ, running));
        }

        public Task<bool> NewMoveXYAsync(ushort xDst, ushort yDst, bool optimized, int accuracy, bool running)
        {
            return MoveXYZAsync(xDst, yDst, 0, accuracy, 255, running);
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

        public Task SetBadLocationAsync(ushort x, ushort y)
        {
            return Client.SendPacketAsync(PacketType.SCSetBadLocation, (x, y));
        }

        public Task SetBadObjectAsync(ushort objType, ushort color, byte radius)
        {
            return Client.SendPacketAsync(PacketType.SCSetBadObject, (objType, color, radius));
        }

        public Task SetGoodLocationAsync(ushort x, ushort y)
        {
            return Client.SendPacketAsync(PacketType.SCSetGoodLocation, (x, y));
        }

        public Task<byte> StepAsync(byte direction, bool running)
        {
            return Client.SendPacketAsync<(byte,bool), byte>(PacketType.SCStep, (direction, running));
        }

        public Task<int> StepQAsync(byte direction, bool running)
        {
            return Client.SendPacketAsync<(byte,bool), int>(PacketType.SCStepQ, (direction, running));
        }
    }
}