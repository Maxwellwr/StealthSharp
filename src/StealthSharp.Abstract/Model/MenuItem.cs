﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class MenuItem
    {
        public ushort Model { get; set; }
        public ushort Color { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}