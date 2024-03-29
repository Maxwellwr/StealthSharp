#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IReflectionMetadata.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;

#endregion

namespace StealthSharp.Serialization
{
    public interface IReflectionMetadata
    {
        IReadOnlyList<PacketProperty> Properties { get; }
        Endianness Endianness { get; }
    }
}