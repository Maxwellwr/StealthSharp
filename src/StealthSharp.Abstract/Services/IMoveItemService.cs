#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMoveItemService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IMoveItemService
    {
        void SetDropDelay(uint value);
        Task<uint> GetDropDelayAsync();

        void SetPickedUpItem(uint value);
        Task<uint> GetPickedUpItemAsync();
        void SetDropCheckCoord(bool value);
        Task<bool> GetDropCheckCoordAsync();
        Task<bool> DragItemAsync(uint itemID, int count);

        Task<bool> DropAsync(uint itemID, int count, int x, int y, int z);

        Task<bool> DropHereAsync(uint itemID);

        Task<bool> DropItemAsync(uint moveIntoID, int x, int y, int z);

        Task<bool> EmptyContainerAsync(uint container, uint destContainer, ushort delayMS);

        Task<bool> GrabAsync(uint itemID, int count);

        Task<bool> MoveItemAsync(uint itemID, int count, uint moveIntoID, int x, int y, int z);

        Task<bool> MoveItemsAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoID, int x, int y, int z,
            int delayMS);

        Task<bool> MoveItemsExAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoID, int x, int y, int z,
            int delayMS, int maxItems);

        void SetCatchBag(uint objectId);

        void UnsetCatchBag();
    }
}