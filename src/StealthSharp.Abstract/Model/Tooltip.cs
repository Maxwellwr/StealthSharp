#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Tooltip.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class Tooltip
    {
        [PacketData(0, 4)] public uint ClilocID { get; set; }
        [PacketData(4, 4)] public int Page { get; set; }
        [PacketData(8, 4)] public int ElemNum { get; set; }
    }
}