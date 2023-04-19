#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GestureService.cs" company="StealthSharp">
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
    public class GestureService : BaseService, IGestureService
    {
        public GestureService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task BowAsync()
        {
            return Client.SendPacketAsync(PacketType.SCBow);
        }

        public Task SaluteAsync()
        {
            return Client.SendPacketAsync(PacketType.SCSalute);
        }
    }
}