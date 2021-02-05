#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GestureService.cs" company="StealthSharp">
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
    public class GestureService : BaseService, IGestureService
    {
        public GestureService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public void Bow()
        {
            Client.SendPacket(PacketType.SCBow);
        }

        public void Salute()
        {
            Client.SendPacket(PacketType.SCSalute);
        }
    }
}