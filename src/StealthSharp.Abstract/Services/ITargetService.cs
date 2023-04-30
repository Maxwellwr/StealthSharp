#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ITargetService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface ITargetService
    {
        Task<TargetInfo> GetClientTargetResponseAsync();

        Task<bool> GetClientTargetResponsePresentAsync();

        Task<uint> GetTargetIdAsync();

        Task<bool> GetTargetPresentAsync();

        Task<uint> GetLastTargetAsync();

        Task CancelTargetAsync();

        Task CancelWaitTargetAsync();

        Task<bool> CheckLOSAsync(WorldPoint3D from, WorldPoint3D to, byte worldNum, LOSCheckType checkType, LOSOptions options);

        Task ClientRequestObjectTargetAsync();

        Task ClientRequestTileTargetAsync();

        Task TargetToObjectAsync(uint objectId);

        Task TargetToTileAsync(ushort tileModel, WorldPoint3D point);

        Task TargetToXYZAsync(WorldPoint3D point);

        Task<bool> WaitForClientTargetResponseAsync(int maxWaitTimeMs);

        Task<bool> WaitForTargetAsync(int maxWaitTimeMs);

        Task WaitTargetGroundAsync(ushort objType);

        Task WaitTargetLastAsync();

        Task WaitTargetObjectAsync(uint objId);

        Task WaitTargetSelfAsync();

        Task WaitTargetTileAsync(ushort tile, WorldPoint3D point);

        Task WaitTargetTypeAsync(ushort objType);

        Task WaitTargetXYZAsync(WorldPoint3D point);
    }
}