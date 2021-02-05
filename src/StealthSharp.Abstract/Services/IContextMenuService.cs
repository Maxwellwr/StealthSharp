﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IContextMenuService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Model;

namespace StealthSharp.Services
{
    public interface IContextMenuService
    {
        Task ClearContextMenuAsync();

        Task<List<string>> GetContextMenuAsync();
        
        Task<ContextMenu> GetContextMenuRecAsync();

        Task RequestContextMenuAsync(uint iD);

        Task SetContextMenuHookAsync(uint menuID, byte entryNumber);
    }
}