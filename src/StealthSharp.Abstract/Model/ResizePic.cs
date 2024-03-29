﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ResizePic.cs" company="StealthSharp">
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
    public class ResizePic
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int GumpId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}