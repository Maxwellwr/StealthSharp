﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TextEntryLimited.cs" company="StealthSharp">
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
    public class TextEntryLimited
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Color { get; set; }
        public int ReturnValue { get; set; }
        public int DefaultTextId { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}