#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MapService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class MapService : BaseService, IMapService
    {
        public MapService(IStealthSharpClient client)
            : base(client)
        {
        }

        public Task<uint> AddFigureAsync(MapFigure figure)
        {
            return Client.SendPacketAsync<MapFigure, uint>(PacketType.SCAddFigure, figure);
        }

        public Task ClearFiguresAsync()
        {
            return Client.SendPacketAsync(PacketType.SCClearFigures);
        }

        public Task<bool> RemoveFigureAsync(uint id)
        {
            return Client.SendPacketAsync<uint, bool>(PacketType.SCRemoveFigure,
                id);
        }

        public Task<bool> UpdateFigureAsync(IdMapFigure figure)
        {
            return Client.SendPacketAsync<IdMapFigure, bool>(PacketType.SCUpdateFigure,
                figure);
        }
    }
}