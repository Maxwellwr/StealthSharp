#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ResizePic.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class ResizePic
    {
        [PacketData(0, 4)] public int X { get; set; }
        [PacketData(4, 4)] public int Y { get; set; }
        [PacketData(8, 4)] public int GumpId { get; set; }
        [PacketData(12, 4)] public int Width { get; set; }
        [PacketData(16, 4)] public int Height { get; set; }
        [PacketData(20, 4)] public int Page { get; set; }
        [PacketData(24, 4)] public int ElemNum { get; set; }
    }
}