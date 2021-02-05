#region Copyright

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
        void ClearContextMenu();

        Task<List<string>> GetContextMenuAsync();
        
        Task<ContextMenu> GetContextMenuRecAsync();

        void RequestContextMenu(uint iD);

        void SetContextMenuHook(uint menuID, byte entryNumber);
    }
}