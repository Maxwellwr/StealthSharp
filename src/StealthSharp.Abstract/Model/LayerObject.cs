﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="LayerObject.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Enumeration;

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class LayerObject
    {
        public Layer Layer { get; set; }
        public uint ItemId { get; set; }
    }
}