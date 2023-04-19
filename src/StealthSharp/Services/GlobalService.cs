#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GlobalService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class GlobalService : BaseService, IGlobalService
    {
        public GlobalService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task SetGlobalAsync(VarRegion globalRegion, string varName, string varValue)
        {
            return Client.SendPacketAsync(PacketType.SCSetGlobal, (globalRegion, varName, varValue));
        }

        public Task<string> GetGlobalAsync(VarRegion globalRegion, string varName)
        {
            return Client.SendPacketAsync<(VarRegion, string), string>(PacketType.SCGetGlobal, (globalRegion, varName));
        }
    }
}