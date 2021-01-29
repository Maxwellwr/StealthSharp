#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TargetService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class TargetService : BaseService, ITargetService
    {
        public TargetService(IStealthSharpClient<ushort, uint, ushort> client)
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
            return (await GetTargetIdAsync()) > 0;
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

        public Task<bool> CheckLOSAsync(ushort xf, ushort yf, sbyte zf, ushort xt, ushort yt, sbyte zt, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, sbyte, ushort, ushort, sbyte, byte),bool>(PacketType.SCCheckLOS,
                (xf, yf, zf, xt, yt, zt, worldNum));
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

        public Task TargetToTileAsync(ushort tileModel, ushort x, ushort y, sbyte z)
        {
            return Client.SendPacketAsync(PacketType.SCTargetToTile, (tileModel, x, y, z));
        }

        public Task TargetToXYZAsync(ushort x, ushort y, sbyte z)
        {
            return Client.SendPacketAsync(PacketType.SCTargetToXYZ, (x, y, z));
        }

        public async Task<bool> WaitForClientTargetResponseAsync(int maxWaitTimeMs)
        {
            var enddate = DateTime.Now.AddMilliseconds(maxWaitTimeMs);

            while (DateTime.Now < enddate && !await GetClientTargetResponsePresentAsync())
            {
            }

            return await GetClientTargetResponsePresentAsync();
        }

        public async Task<bool> WaitForTargetAsync(int maxWaitTimeMs)
        {
            var endTime = DateTime.Now.AddMilliseconds(maxWaitTimeMs);
            while (await GetTargetIdAsync() == 0 && DateTime.Now < endTime)
            {
            }

            return await GetTargetIdAsync() > 0;
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

        public Task WaitTargetTileAsync(ushort tile, ushort x, ushort y, sbyte z)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetTile, (tile, x, y, z));
        }

        public Task WaitTargetTypeAsync(ushort objType)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetType, objType);
        }

        public Task WaitTargetXYZAsync(ushort x, ushort y, sbyte z)
        {
            return Client.SendPacketAsync(PacketType.SCWaitTargetXYZ, (x, y, z));
        }
    }
}