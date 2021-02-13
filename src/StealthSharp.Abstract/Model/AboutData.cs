#region Copyright

// -----------------------------------------------------------------------
// <copyright file="AboutData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model
{
    /// <summary>
    ///     About data.
    /// </summary>
    [Serialization.Serializable()]
    public class AboutData
    {
         public ushort[] StealthVersion { get; set; } = new ushort[0];
         public ushort Build { get; set; }
         public DateTime BuildDate { get; set; }
         public ushort GitRevNumber { get; set; }
        
        public string GitRevision { get; set; } = "";
    }
}