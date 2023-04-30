#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TargetService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class TargetService : BaseService, ITargetService
    {
        public TargetService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<TargetInfo> GetClientTargetResponseAsync()
        {
            return Client.SendPacketAsync<TargetInfo>(PacketType.SCClientTargetResponse);
        }

        public Task<bool> GetClientTargetResponsePresentAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCClientTargetResponsePresent);
        }

        public Task<uint> GetTargetIdAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetTargetID);
        }

        public async Task<bool> GetTargetPresentAsync()
        {
            return await GetTargetIdAsync().ConfigureAwait(false) > 0;
        }

        public Task<uint> GetLastTargetAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastTarget);
        }

        public Task CancelTargetAsync()
        {
            return Client.SendPacketAsync(PacketType.SCCancelTarget);
        }

        public Task CancelWaitTargetAsync()
        {
            return Client.SendPacketAsync(PacketType.SCCancelWaitTarget);
        }

        public Task<bool> CheckLOSAsync(WorldPoint3D from, WorldPoint3D to, byte worldNum, LOSCheckType checkType, LOSOptions options)
        {
            return Client.SendPacketAsync<(WorldPoint3D, WorldPoint3D, byte, LOSCheckType, LOSOptions), bool>(PacketType.SCCheckLOS,
                (from, to, worldNum, checkType, options));
        }

        public Task ClientRequestObjectTargetAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClientRequestObjectTarget);
        }

        public Task ClientRequestTileTargetAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClientRequestTileTarget);
        }

        public Task TargetToObjectAsync(uint objectId)
        {
            return Client.SendPacketAsync(PacketType.SCTargetToObject, objectId);
        }

        public Task TargetToTileAsync(ushort tileModel, WorldPoint3D point)
        {
            return Client.SendPacketAsync(PacketType.SCTargetToTile, (tileModel, point));
        }

        public Task TargetToXYZAsync(WorldPoint3D point)
        {
            return Client.SendPacketAsync(PacketType.SCTargetToXYZ, point);
        }

        public async Task<bool> WaitForClientTargetResponseAsync(int maxWaitTimeMs)
        {
            var enddate = DateTime.Now.AddMilliseconds(maxWaitTimeMs);

            while (DateTime.Now < enddate && !await GetClientTargetResponsePresentAsync().ConfigureAwait(false))
            {
            }

            return await GetClientTargetResponsePresentAsync().ConfigureAwait(false);
        }

        public async Task<bool> WaitForTargetAsync(int maxWaitTimeMs)
        {
            var endTime = DateTime.Now.AddMilliseconds(maxWaitTimeMs);
            while (await GetTargetIdAsync().ConfigureAwait(false) == 0 && DateTime.Now < endTime)
            {
            }

            return await GetTargetIdAsync().ConfigureAwait(false) > 0;
        }

        public Task WaitTargetGroundAsync(ushort objType)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetGround, objType);
        }

        public Task WaitTargetLastAsync()
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetLast);
        }

        public Task WaitTargetObjectAsync(uint objId)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetObject, objId);
        }

        public Task WaitTargetSelfAsync()
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetSelf);
        }

        public Task WaitTargetTileAsync(ushort tile, WorldPoint3D point)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetTile, (tile, point));
        }

        public Task WaitTargetTypeAsync(ushort objType)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetType, objType);
        }

        public Task WaitTargetXYZAsync(WorldPoint3D point)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetXYZ, point);
        }
    }
}