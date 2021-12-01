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
        Task SetDropDelayAsync(uint value);
        Task<uint> GetDropDelayAsync();

        Task SetPickedUpItemAsync(uint value);
        Task<uint> GetPickedUpItemAsync();
        Task SetDropCheckCoordAsync(bool value);
        Task<bool> GetDropCheckCoordAsync();
        Task<bool> DragItemAsync(uint itemId, int count);

        Task<bool> DropAsync(uint itemId, int count, int x, int y, int z);

        Task<bool> DropHereAsync(uint itemId);

        Task<bool> DropItemAsync(uint moveIntoId, int x, int y, int z);

        Task<bool> EmptyContainerAsync(uint container, uint destContainer, ushort delayMs);

        Task<bool> GrabAsync(uint itemId, int count);

        Task<bool> MoveItemAsync(uint itemId, int count, uint moveIntoId, int x, int y, int z);

        Task<bool> MoveItemsAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId, int x, int y, int z,
            int delayMs);

        Task<bool> MoveItemsExAsync(uint container, ushort itemsType, ushort itemsColor, uint moveIntoId, int x, int y, int z,
            int delayMs, int maxItems);

        Task SetCatchBagAsync(uint objectId);

        Task UnsetCatchBagAsync();
    }
}