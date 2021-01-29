#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TestData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Benchmark
{
    public class Packet<T>
    {
        [PacketData(0, 4, PacketDataType = PacketDataType.Length)]
        public uint Length { get; set; }

        [PacketData(4, 2, PacketDataType = PacketDataType.MetaData)]
        public ushort Method { get; set; }

        [PacketData(6, 2, PacketDataType = PacketDataType.Id)]
        public ushort ReturnId { get; set; }

        [PacketData(8, PacketDataType = PacketDataType.Body)]
        public T Body { get; set; }
    }

    public class AboutData
    {
        [PacketData(0, 2)] public ushort Property1 { get; set; }
        [PacketData(2, 2)] public ushort Property2 { get; set; }
        [PacketData(4, 2)] public ushort Property3 { get; set; }
    }
}