#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketDataType.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Serialization
{
    public enum PacketDataType : byte
    {
        /// <summary>
        ///     Metadata property or field
        /// </summary>
        MetaData,

        /// <summary>
        ///     Packet id property or field
        /// </summary>
        Id,

        /// <summary>
        ///     Packet length property or field
        /// </summary>
        Length,

        /// <summary>
        ///     Packet body
        /// </summary>
        Body,

        /// <summary>
        ///     Field for TypeMapper
        /// </summary>
        TypeMapper,

        /// <summary>
        ///     Dynamic arrays
        /// </summary>
        Dynamic
    }
}