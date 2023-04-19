#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ClilocItemRec.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class ClilocItemRec
    {
        public uint ClilocId { get; set; }


        public List<string> Params { get; set; } = new();
    }
}