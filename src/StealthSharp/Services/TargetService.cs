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

        public void CancelTarget()
        {
            Client.SendPacket(PacketType.SCCancelTarget);
        }

        public void CancelWaitTarget()
        {
            Client.SendPacket(PacketType.SCCancelWaitTarget);
        }

        public Task<bool> CheckLOSAsync(ushort xf, ushort yf, sbyte zf, ushort xt, ushort yt, sbyte zt, byte worldNum)
        {
            return Client.SendPacketAsync<(ushort, ushort, sbyte, ushort, ushort, sbyte, byte),bool>(PacketType.SCCheckLOS,
                (xf, yf, zf, xt, yt, zt, worldNum));
        }

        public void ClientRequestObjectTarget()
        {
            Client.SendPacket(PacketType.SCClientRequestObjectTarget);
        }

        public void ClientRequestTileTarget()
        {
            Client.SendPacket(PacketType.SCClientRequestTileTarget);
        }

        public void TargetToObject(uint objectId)
        {
            Client.SendPacket(PacketType.SCTargetToObject, objectId);
        }

        public void TargetToTile(ushort tileModel, ushort x, ushort y, sbyte z)
        {
            Client.SendPacket(PacketType.SCTargetToTile, (tileModel, x, y, z));
        }

        public void TargetToXYZ(ushort x, ushort y, sbyte z)
        {
            Client.SendPacket(PacketType.SCTargetToXYZ, (x, y, z));
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

        public void WaitTargetGround(ushort objType)
        {
            Client.SendPacket(PacketType.SCWaitTargetGround, objType);
        }

        public void WaitTargetLast()
        {
            Client.SendPacket(PacketType.SCWaitTargetLast);
        }

        public void WaitTargetObject(uint objId)
        {
            Client.SendPacket(PacketType.SCWaitTargetObject, objId);
        }

        public void WaitTargetSelf()
        {
            Client.SendPacket(PacketType.SCWaitTargetSelf);
        }

        public void WaitTargetTile(ushort tile, ushort x, ushort y, sbyte z)
        {
            Client.SendPacket(PacketType.SCWaitTargetTile, (tile, x, y, z));
        }

        public void WaitTargetType(ushort objType)
        {
            Client.SendPacket(PacketType.SCWaitTargetType, objType);
        }

        public void WaitTargetXYZ(ushort x, ushort y, sbyte z)
        {
            Client.SendPacket(PacketType.SCWaitTargetXYZ, (x, y, z));
        }
    }
}