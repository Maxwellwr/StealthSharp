#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MyPoint.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     My Point.
    /// </summary>
    public class MyPoint
    {
        [PacketData(0, 2)] public ushort X { get; set; }
        [PacketData(2, 2)] public ushort Y { get; set; }
        [PacketData(4, 1)] public sbyte Z { get; set; }
    }
}