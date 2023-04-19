#region Copyright

// -----------------------------------------------------------------------
// <copyright file="CheckBox.cs" company="StealthSharp">
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
    public class CheckBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ReleasedId { get; set; }
        public int PressedId { get; set; }
        public int Status { get; set; }
        public int ReturnValue { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}