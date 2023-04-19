#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MultiItem.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class MultiItem
    {
        public uint Id { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
        public sbyte Z { get; set; }
        public ushort XMin { get; set; }
        public ushort XMax { get; set; }
        public ushort YMin { get; set; }
        public ushort YMax { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
    }
}