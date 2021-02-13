#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MapFigure.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Enum;

namespace StealthSharp.Model
{
    public class IdMapFigure
    {
        public uint Id { get; set; }
        public FigureKind Kind { get; set; }
        public FigureCoord Coord { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public uint BrushColor { get; set; }
        public BrushStyle BrushStyle { get; set; }
        public uint Color { get; set; }
         public string Text { get; set; }
    }
}