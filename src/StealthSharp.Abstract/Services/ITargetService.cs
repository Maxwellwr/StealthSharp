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

        void CancelTarget();

        void CancelWaitTarget();

        Task<bool> CheckLOSAsync(ushort xf, ushort yf, sbyte zf, ushort xt, ushort yt, sbyte zt, byte worldNum);

        void ClientRequestObjectTarget();

        void ClientRequestTileTarget();

        void TargetToObject(uint objectID);

        void TargetToTile(ushort tileModel, ushort x, ushort y, sbyte z);

        void TargetToXYZ(ushort x, ushort y, sbyte z);

        Task<bool> WaitForClientTargetResponseAsync(int maxWaitTimeMS);

        Task<bool> WaitForTargetAsync(int maxWaitTimeMS);

        void WaitTargetGround(ushort objType);

        void WaitTargetLast();

        void WaitTargetObject(uint objID);

        void WaitTargetSelf();

        void WaitTargetTile(ushort tile, ushort x, ushort y, sbyte z);

        void WaitTargetType(ushort objType);

        void WaitTargetXYZ(ushort x, ushort y, sbyte z);
    }
}