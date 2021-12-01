#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GumpButton.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class GumpButton
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ReleasedId { get; set; }
        public int PressedId { get; set; }
        public int Quit { get; set; }
        public int PageId { get; set; }
        public int ReturnValue { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}