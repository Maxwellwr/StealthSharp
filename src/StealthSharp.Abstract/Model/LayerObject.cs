#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LayerObject.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Enumeration;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class LayerObject
    {
        public Layer Layer { get; set; }
        public uint ItemId { get; set; }
    }
}