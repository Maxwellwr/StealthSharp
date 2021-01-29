#region Copyright

// -----------------------------------------------------------------------
// <copyright file="BuffIcon.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class BuffIcon
    {
        [PacketData(0, 2)] public ushort Attribute_ID { get; set; }
        [PacketData(2, 8)] public DateTime TimeStart { get; set; }
        [PacketData(10, 2)] public ushort Seconds { get; set; }
        [PacketData(12, 4)] public uint ClilocID1 { get; set; }
        [PacketData(16, 4)] public uint ClilocID2 { get; set; }
    }
}