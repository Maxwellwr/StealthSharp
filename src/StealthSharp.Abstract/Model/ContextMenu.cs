#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ContextMenu.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public struct ContextMenu
    {
        public uint Id { get; set; }

        public byte EntriesNumber { get; set; }

        public bool NewCliloc { get; set; }

        
        public List<ContextMenuEntry> Entries { get; set; }
    }
}