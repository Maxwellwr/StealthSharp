#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="PathRequest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class PathReqeust
    {
        [PacketData(0, 2)] public ushort StartX { get; set; }
        [PacketData(2, 2)] public ushort StartY { get; set; }
        [PacketData(4, 1)] public sbyte StartZ { get; set; }
        [PacketData(5, 2)] public ushort FinishX { get; set; }
        [PacketData(7, 2)] public ushort FinishY { get; set; }
        [PacketData(9, 1)] public sbyte FinishZ { get; set; }
        [PacketData(10, 1)] public byte WorldNum { get; set; }
        [PacketData(11, 4)] public int AccuracyXy { get; set; }
        [PacketData(15, 4)] public int AccuracyZ { get; set; }
        [PacketData(19, 1)] public bool Run { get; set; }
    }
}