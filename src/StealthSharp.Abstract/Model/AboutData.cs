#region Copyright

// -----------------------------------------------------------------------
// <copyright file="AboutData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     About data.
    /// </summary>
    public class AboutData
    {
        [PacketData(0, 10)] public ushort[] StealthVersion { get; set; } = new ushort[0];
        [PacketData(10, 2)] public ushort Build { get; set; }
        [PacketData(12, 8)] public DateTime BuildDate { get; set; }
        [PacketData(20, 2)] public ushort GitRevNumber { get; set; }
        [PacketData(22, PacketDataType = PacketDataType.Body)]
        public string GitRevision { get; set; } = "";
    }
}