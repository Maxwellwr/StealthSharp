#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ClilocItemRec.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;

namespace StealthSharp.Model
{
    public class ClilocItemRec
    {
        public uint ClilocID { get; set; }

        
        public List<string> Params { get; set; }
    }
}