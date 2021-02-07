#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ILayerService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;

namespace StealthSharp.Services
{
    public interface ILayerService
    {
        Task SetDressSpeedAsync(ushort value);
        Task<ushort> GetDressSpeedAsync();

        Task<bool> DisarmAsync();

        Task<bool> DressSavedSetAsync();

        Task<bool> EquipAsync(Layer layer, uint objId);

        Task<bool> EquipDressSetAsync();

        Task<bool> EquiptAsync(Layer layer, ushort objType);

        Task<Layer> GetLayerAsync(uint objId);

        /// <summary>
        ///     Returns Object ID located on your Char to specify the layer LayerType
        ///     If there is no connection to the UO server, or in the bed wearing nothing - returns 0.
        /// </summary>
        /// <param name="layerType">Layer to search.</param>
        /// <returns>Object id.</returns>
        Task<uint> ObjAtLayerAsync(Layer layerType);

        /// <summary>
        ///     Returns Object ID located on your Char to specify the layer LayerType and playerId
        ///     If there is no connection to the UO server, or in the bed wearing nothing - returns 0.
        /// </summary>
        /// <param name="layerType">Layer.</param>
        /// <param name="playerId">Player.</param>
        /// <returns>Object id.</returns>
        Task<uint> ObjAtLayerExAsync(Layer layerType, uint playerId);

        Task SetDressAsync();

        Task<bool> UndressAsync();

        Task<bool> UnequipAsync(Layer layer);

        Task<bool> WearItemAsync(Layer layer, uint objId);
    }
}