#region Copyright

// -----------------------------------------------------------------------
// <copyright file="BuffIcon.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class BuffIcon
    {
        public ushort AttributeId { get; set; }
        public DateTime TimeStart { get; set; }
        public ushort Seconds { get; set; }
        public uint ClilocId1 { get; set; }
        public uint ClilocId2 { get; set; }
    }
}