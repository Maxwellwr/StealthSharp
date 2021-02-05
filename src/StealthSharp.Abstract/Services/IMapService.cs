#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMapService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Model;

namespace StealthSharp.Services
{
    public interface IMapService
    {
        Task<uint> AddFigureAsync(MapFigure figure);

        void ClearFigures();

        Task<bool> RemoveFigureAsync(uint id);

        Task<bool> UpdateFigureAsync(IdMapFigure figure);
    }
}