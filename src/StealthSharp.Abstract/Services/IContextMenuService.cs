#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IContextMenuService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface IContextMenuService
    {
        Task ClearContextMenuAsync();

        Task<List<string>> GetContextMenuAsync();

        Task<ContextMenu> GetContextMenuRecAsync();

        Task RequestContextMenuAsync(uint id);

        Task SetContextMenuHookAsync(uint menuId, byte entryNumber);
    }
}