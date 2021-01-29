#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MapCell.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     Map Cell.
    /// </summary>
    public struct MapCell
    {
        [PacketData(0, 2)] public ushort Tile { get; set; }
        [PacketData(2, 1)] public sbyte Z { get; set; }
    }
}