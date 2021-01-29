#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ContextMenu.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public struct ContextMenu
    {
        [PacketData(0, 4)] public uint ID { get; set; }

        [PacketData(4, 1)] public byte EntriesNumber { get; set; }

        [PacketData(5, 1)] public bool NewCliloc { get; set; }

        [PacketData(6, PacketDataType = PacketDataType.Body)]
        public List<ContextMenuEntry> Entries { get; set; }
    }
}