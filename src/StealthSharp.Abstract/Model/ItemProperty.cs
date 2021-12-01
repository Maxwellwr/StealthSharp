#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ItemProperty.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class ItemProperty
    {
        public uint Prop { get; set; }
        public int ElemNum { get; set; }
    }
}