#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TestData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Enum;
using StealthSharp.Network;
using StealthSharp.Serialization;

namespace StealthSharp.Benchmark
{
    [Serializable()]
    public class AboutData
    {
        public ushort Property1 { get; set; }
        public ushort Property2 { get; set; }
        public ushort Property3 { get; set; }
    }
}