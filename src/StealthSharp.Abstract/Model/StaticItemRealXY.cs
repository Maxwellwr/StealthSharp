#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StaticItemRealXY.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     StaticItemRealXY.
    /// </summary>
    public class StaticItemRealXY
    {
        [PacketData(0, 2)] public ushort Tile { get; set; }
        [PacketData(2, 2)] public ushort X { get; set; }
        [PacketData(4, 2)] public ushort Y { get; set; }
        [PacketData(6, 1)] public sbyte Z { get; set; }
        [PacketData(7, 2)] public ushort Color { get; set; }
    }
}