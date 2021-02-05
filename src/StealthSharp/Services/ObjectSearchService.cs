#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ObjectSearchService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class ObjectSearchService : BaseService, IObjectSearchService
    {
        public ObjectSearchService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public Task<uint> GetBackpackAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetBackpackID);
        }

        public Task<int> GetFindCountAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetFindCount);
        }

        public void SetFindDistance(uint value)
        {
            Client.SendPacket(PacketType.SCSetFindDistance, value);
        }

        public Task<uint> GetFindDistanceAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetFindDistance);
        }

        public Task<List<uint>> GetFindedListAsync()
        {
            return Client.SendPacketAsync<List<uint>>(PacketType.SCGetFindedList);
        }

        public Task<int> GetFindFullQuantityAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetFindFullQuantity);
        }

        public void SetFindInNulPoint(bool value)
        {
            Client.SendPacket(PacketType.SCSetFindInNulPoint, value);
        }

        public Task<bool> GetFindInNulPointAsync()
        {
            return Client.SendPacketAsync<bool>(PacketType.SCGetFindInNulPoint);
        }

        public Task<uint> GetFindItemAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetFindItem);
        }

        public Task<int> GetFindQuantityAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetFindQuantity);
        }

        public void SetFindVertical(int value)
        {
            Client.SendPacket(PacketType.SCSetFindVertical, value);
        }

        public Task<int> GetFindVerticalAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetFindVertical);
        }

        public Task<uint> GetGroundAsync()
        {
            return Task.FromResult(0x00u);
        }

        public Task<List<uint>> GetIgnoreListAsync()
        {
            return Client.SendPacketAsync<List<uint>>(PacketType.SCGetIgnoreList);
        }

        public Task<uint> GetLastContainerAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastContainer);
        }

        public Task<uint> GetLastObjectAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLastObject);
        }

        public async Task<int> CountAsync(ushort objType)
        {
            await FindTypeAsync(objType, await GetBackpackAsync());
            return await GetFindFullQuantityAsync();
        }

        public async Task<int> CountExAsync(ushort objType, ushort color, uint container)
        {
            await FindTypeExAsync(objType, color, container, false);
            return await GetFindFullQuantityAsync();
        }

        public async Task<int> CountGroundAsync(ushort objType)
        {
            await FindTypeAsync(objType, await GetGroundAsync());
            return await GetFindFullQuantityAsync();
        }

        public Task<uint> FindAtCoordAsync(ushort x, ushort y)
        {
            return Client.SendPacketAsync<(ushort, ushort), uint>(PacketType.SCFindAtCoord, (x, y));
        }

        public Task<uint> FindNotorietyAsync(ushort objType, byte notoriety)
        {
            return Client.SendPacketAsync<(ushort, byte), uint>(PacketType.SCFindNotoriety, (objType, notoriety));
        }

        public Task<uint> FindTypeAsync(ushort objType, uint container)
        {
            return FindTypeExAsync(objType, 0xFFFF, container, false);
        }

        public Task<uint> FindTypeExAsync(ushort objType, ushort color, uint container, bool inSub)
        {
            return Client.SendPacketAsync<(ushort, ushort, uint, bool), uint>(PacketType.SCFindTypeEx,
                (objType, color, container, inSub));
        }

        public Task<uint> FindTypesArrayExAsync(ushort[] objTypes, ushort[] colors, uint[] containers, bool inSub)
        {
            return Client.SendPacketAsync<(ushort[], ushort[], uint[], bool), uint>(PacketType.SCFindTypesArrayEx,
                (objTypes, colors, containers, inSub));
        }

        public Task<List<MultiItem>> GetMultisAsync()
        {
            return Client.SendPacketAsync<List<MultiItem>>(PacketType.SCGetMultis);
        }

        public void Ignore(uint objId)
        {
            Client.SendPacket(PacketType.SCIgnore, objId);
        }

        public void IgnoreOff(uint objId)
        {
            Client.SendPacket(PacketType.SCIgnoreOff, objId);
        }

        public void IgnoreReset()
        {
            Client.SendPacket(PacketType.SCIgnoreReset);
        }
    }
}