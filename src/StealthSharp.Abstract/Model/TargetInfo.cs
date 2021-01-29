#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TargetInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     Target Info.
    /// </summary>
    public class TargetInfo
    {
        [PacketData(0, 4)] public uint ID { get; set; }
        [PacketData(4, 2)] public ushort Tile { get; set; }
        [PacketData(6, 2)] public ushort X { get; set; }
        [PacketData(8, 2)] public ushort Y { get; set; }
        [PacketData(10, 1)] public sbyte Z { get; set; }
    }
}