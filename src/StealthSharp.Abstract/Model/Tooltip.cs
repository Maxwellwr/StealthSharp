#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Tooltip.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class Tooltip
    {
        public uint ClilocId { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}