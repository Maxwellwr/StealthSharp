﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Group.cs" company="StealthSharp">
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
    public class Group
    {
        public int GroupNumber { get; set; }
        public int Page { get; set; }
        public int ElemNum { get; set; }
    }
}