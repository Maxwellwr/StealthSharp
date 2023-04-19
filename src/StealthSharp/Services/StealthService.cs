#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class StealthService : BaseService, IStealthService
    {
        public StealthService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<string> GetCurrentScriptPathAsync()
        {
            return Task.FromResult(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "");
        }

        public Task<string> GetProfileNameAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetProfileName);
        }

        public Task<string> GetProfileShardNameAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetProfileShardName);
        }

        public Task<AboutData> GetStealthInfoAsync()
        {
            return Client.SendPacketAsync<AboutData>(PacketType.SCGetStealthInfo);
        }

        public Task<string> GetStealthPathAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetStealthPath);
        }

        public Task<string> GetStealthProfilePathAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetStealthProfilePath);
        }

        public Task<string> GetShardNameAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetShardName);
        }

        public Task<string> GetShardPathAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetShardPath);
        }
    }
}