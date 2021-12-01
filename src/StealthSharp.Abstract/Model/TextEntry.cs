#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TextEntry.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class TextEntry
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Color { get; set; }
        public int ReturnValue { get; set; }
        public int DefaultTextId { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}