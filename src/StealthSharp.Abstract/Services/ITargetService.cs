#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ITargetService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Model;

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

        Task<bool> CheckLOSAsync(ushort xf, ushort yf, sbyte zf, ushort xt, ushort yt, sbyte zt, byte worldNum);

        Task ClientRequestObjectTargetAsync();

        Task ClientRequestTileTargetAsync();

        Task TargetToObjectAsync(uint objectID);

        Task TargetToTileAsync(ushort tileModel, ushort x, ushort y, sbyte z);

        Task TargetToXYZAsync(ushort x, ushort y, sbyte z);

        Task<bool> WaitForClientTargetResponseAsync(int maxWaitTimeMS);

        Task<bool> WaitForTargetAsync(int maxWaitTimeMS);

        Task WaitTargetGroundAsync(ushort objType);

        Task WaitTargetLastAsync();

        Task WaitTargetObjectAsync(uint objID);

        Task WaitTargetSelfAsync();

        Task WaitTargetTileAsync(ushort tile, ushort x, ushort y, sbyte z);

        Task WaitTargetTypeAsync(ushort objType);

        Task WaitTargetXYZAsync(ushort x, ushort y, sbyte z);
    }
}