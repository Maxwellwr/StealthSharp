#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ContextMenu.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public struct ContextMenu
    {
        public uint Id { get; set; }

        public byte EntriesNumber { get; set; }

        public bool NewCliloc { get; set; }


        public List<ContextMenuEntry> Entries { get; set; }
    }
}