#region Copyright

// -----------------------------------------------------------------------
// <copyright file="InfoWindowService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class InfoWindowService : BaseService, IInfoWindowService
    {
        public InfoWindowService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task ClearInfoWindowAsync()
        {
             return Client.SendPacketAsync(PacketType.SCClearInfoWindow);
        }

        public Task FillInfoWindowAsync(string str)
        {
             return Client.SendPacketAsync(PacketType.SCFillNewWindow, str);
        }
    }
}