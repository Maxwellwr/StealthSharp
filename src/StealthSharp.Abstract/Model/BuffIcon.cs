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
    public class BuffIcon
    {
        public ushort Attribute_ID { get; set; }
        public DateTime TimeStart { get; set; }
        public ushort Seconds { get; set; }
        public uint ClilocID1 { get; set; }
        public uint ClilocID2 { get; set; }
    }
}