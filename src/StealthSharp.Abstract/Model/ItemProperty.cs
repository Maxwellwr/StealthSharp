#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ItemProperty.cs" company="StealthSharp">
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
    public class ItemProperty
    {
        public uint Prop { get; set; }
        public int ElemNum { get; set; }
    }
}