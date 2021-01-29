#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MapFigure.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Enum;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class IdMapFigure
    {
        [PacketData(0, 4)] public uint Id { get; set; }
        [PacketData(4, 1)] public FigureKind Kind { get; set; }
        [PacketData(5, 1)] public FigureCoord Coord { get; set; }
        [PacketData(6, 4)] public int X1 { get; set; }
        [PacketData(10, 4)] public int Y1 { get; set; }
        [PacketData(14, 4)] public int X2 { get; set; }
        [PacketData(18, 4)] public int Y2 { get; set; }
        [PacketData(22, 4)] public uint BrushColor { get; set; }
        [PacketData(26, 1)] public BrushStyle BrushStyle { get; set; }
        [PacketData(27, 4)] public uint Color { get; set; }
        [PacketData(31, PacketDataType = PacketDataType.Body)] public string Text { get; set; }
    }
}