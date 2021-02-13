#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MapCell.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     Map Cell.
    /// </summary>
    public struct MapCell
    {
        public ushort Tile { get; set; }
        public sbyte Z { get; set; }
    }
}