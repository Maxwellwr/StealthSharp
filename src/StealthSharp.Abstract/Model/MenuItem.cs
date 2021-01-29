#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class MenuItem
    {
        [PacketData(0, 2)] public ushort Model { get; set; }
        [PacketData(2, 2)] public ushort Color { get; set; }

        [PacketData(4, PacketDataType = PacketDataType.Body)]
        public string Text { get; set; }
    }
}