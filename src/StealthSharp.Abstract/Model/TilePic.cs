#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TilePic.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class TilePic
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Id { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}