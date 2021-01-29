#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ClilocItemRec.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class ClilocItemRec
    {
        [PacketData(0, 4)] public uint ClilocID { get; set; }

        [PacketData(4, PacketDataType = PacketDataType.Body)]
        public List<string> Params { get; set; }
    }
}