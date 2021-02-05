#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GlobalService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class GlobalService : BaseService, IGlobalService
    {
        public GlobalService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public void SetGlobal(VarRegion globalRegion, string varName, string varValue)
        {
             Client.SendPacket(PacketType.SCSetGlobal, (globalRegion, varName, varValue));
        }

        public Task<string> GetGlobalAsync(VarRegion globalRegion, string varName)
        {
            return Client.SendPacketAsync<(VarRegion, string), string>(PacketType.SCGetGlobal, (globalRegion, varName));
        }
    }
}