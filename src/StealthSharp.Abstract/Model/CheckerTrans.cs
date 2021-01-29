#region Copyright

// -----------------------------------------------------------------------
// <copyright file="CheckerTrans.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class CheckerTransparency
    {
        [PacketData(0, 4)] public int X;
        [PacketData(4, 4)] public int Y;
        [PacketData(8, 4)] public int Width;
        [PacketData(12, 4)] public int Height;
        [PacketData(16, 4)] public int Page;
        [PacketData(20, 4)] public int ElemNum;
    }
}