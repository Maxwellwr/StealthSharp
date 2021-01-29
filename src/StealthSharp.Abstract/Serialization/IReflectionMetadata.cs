#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IReflectionMetadata.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;

namespace StealthSharp.Serialization
{
    public interface IReflectionMetadata
    {
        IReadOnlyList<PacketProperty> Properties { get; }
        int MetaLength { get; }
        int IdLength { get; }
        int LengthLength { get; }
        int TypeMapperLength { get; }
        PacketProperty this[PacketDataType index] { get; }
        bool Contains(PacketDataType packetDataType);
        IReflectionMetadata? BodyMetadata { get; }
    }
}