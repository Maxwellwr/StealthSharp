#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LayerObject.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Enum;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class LayerObject
    {
        [PacketData(0, 1)] public Layer Layer { get; set; }
        [PacketData(1, 4)] public uint ItemId { get; set; }
    }
}