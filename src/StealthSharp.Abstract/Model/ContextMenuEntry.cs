#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ContextMenuEntry.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public struct ContextMenuEntry
    {
        public ushort Tag { get; set; }

        public uint IntLocId { get; set; }

        public ushort Flags { get; set; }

        public ushort Color { get; set; }
    }
}