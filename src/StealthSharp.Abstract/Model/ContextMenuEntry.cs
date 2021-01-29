#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ContextMenuEntry.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public struct ContextMenuEntry
    {
        [PacketData(0, 2)] public ushort Tag { get; set; }

        [PacketData(2, 4)] public uint IntLocID { get; set; }

        [PacketData(6, 2)] public ushort Flags { get; set; }

        [PacketData(8, 2)] public ushort Color { get; set; }
    }
}