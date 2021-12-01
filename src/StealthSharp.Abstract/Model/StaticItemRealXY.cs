#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StaticItemRealXY.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     StaticItemRealXY.
    /// </summary>
    [Serialization.Serializable()]
    public class StaticItemRealXY
    {
        public ushort Tile { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
        public sbyte Z { get; set; }
        public ushort Color { get; set; }
    }
}