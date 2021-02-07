#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GameObjectService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class GameObjectService : BaseService, IGameObjectService
    {
        public GameObjectService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public Task ClickOnObjectAsync(uint objectId)
        {
            return Client.SendPacketAsync(PacketType.SCClickOnObject, objectId);
        }

        public Task<ushort> GetColorAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, ushort>(PacketType.SCGetColor, objId);
        }

        public Task<string> GetClilocAsync(uint objId)
        {
            return GetTooltipAsync(objId);
        }

        public Task<string> GetClilocByIDAsync(uint clilocId)
        {
            return Client.SendPacketAsync<uint, string>(PacketType.SCGetClilocByID, clilocId);
        }

        public Task<int> GetDexAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetDex, objId);
        }

        public Task<byte> GetDirectionAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, byte>(PacketType.SCGetDirection, objId);
        }

        public Task<int> GetDistanceAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetDistance, objId);
        }

        public Task<int> GetHPAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetHP, objId);
        }

        public Task<int> GetIntAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetInt, objId);
        }

        public Task<int> GetManaAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetMana, objId);
        }

        public Task<int> GetMaxHPAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetMaxHP, objId);
        }

        public Task<int> GetMaxManaAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetMaxMana, objId);
        }

        public Task<int> GetMaxStamAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetMaxStam, objId);
        }

        public Task<string> GetNameAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, string>(PacketType.SCGetName, objId);
        }

        public Task<byte> GetNotorietyAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, byte>(PacketType.SCGetNotoriety, objId);
        }

        public Task<uint> GetParentAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, uint>(PacketType.SCGetParent, objId);
        }

        public Task<int> GetQuantityAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetQuantity, objId);
        }

        public Task<int> GetStamAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetStam, objId);
        }

        public async Task<Bitmap?> GetStaticArtAsync(uint objType, ushort objColor)
        {
            var res = await Client.SendPacketAsync<(uint, ushort), byte[]>(PacketType.SCGetStaticArtBitmap, (objType, objColor));

            if (res.Length == 0)
            {
                return null;
            }

            MemoryStream ms = new(res);
            return new Bitmap(ms);
        }

        public Task<int> GetStrAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, int>(PacketType.SCGetStr, objId);
        }

        public Task<string> GetTooltipAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, string>(PacketType.SCGetCliloc, objId);
        }

        public Task<List<ClilocItemRec>> GetTooltipRecAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, List<ClilocItemRec>>(PacketType.SCGetToolTipRec, objId);
        }

        public Task<ushort> GetTypeAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, ushort>(PacketType.SCGetType, objId);
        }

        public Task<ushort> GetXAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, ushort>(PacketType.SCGetX, objId);
        }

        public Task<ushort> GetYAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, ushort>(PacketType.SCGetY, objId);
        }

        public Task<sbyte> GetZAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, sbyte>(PacketType.SCGetZ, objId);
        }

        public Task<bool> IsContainerAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsContainer, objId);
        }

        public Task<bool> IsDeadAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsDead, objId);
        }

        public Task<bool> IsFemaleAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsFemale, objId);
        }

        public Task<bool> IsHiddenAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsHidden, objId);
        }

        public Task<bool> IsMovableAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsMovable, objId);
        }

        public Task<bool> IsNPCAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsNPC, objId);
        }

        public Task<bool> IsObjectExistsAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsObjectExists, objId);
        }

        public Task<bool> IsParalyzedAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsParalyzed, objId);
        }

        public Task<bool> IsPoisonedAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsPoisoned, objId);
        }

        public Task<bool> IsRunningAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsRunning, objId);
        }

        public Task<bool> IsWarModeAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsWarMode, objId);
        }

        public Task<bool> IsYellowHitsAsync(uint objId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCIsYellowHits, objId);
        }

        public Task<bool> MobileCanBeRenamedAsync(uint mobId)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCMobileCanBeRenamed, mobId);
        }

        public Task RenameMobileAsync(uint mobId, string newName)
        {
            return Client.SendPacketAsync(PacketType.SCRenameMobile, (mobId, newName));
        }

        public Task<uint> UseFromGroundAsync(ushort objType, ushort color)
        {
            return Client.SendPacketAsync<(ushort, ushort), uint>(PacketType.SCUseFromGround, (objType, color));
        }

        public Task UseObjectAsync(uint objId)
        {
            return Client.SendPacketAsync(PacketType.SCUseObject, objId);
        }

        public Task<uint> UseTypeAsync(ushort objType, ushort color = 0xFFFF)
        {
            return Client.SendPacketAsync<(ushort, ushort), uint>(PacketType.SCUseType, (objType, color));
        }

        public Task UseItemOnMobileAsync(uint itemId, uint mobileId)
        {
            return Client.SendPacketAsync(PacketType.SCUseItemOnMobile, (itemId, mobileId));
        }
    }
}