#region Copyright

// -----------------------------------------------------------------------
// <copyright file="XmfHTMLGump.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class XmfHTMLGump
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public uint ClilocId { get; set; }
        public int Background { get; set; }
        public int Scrollbar { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}