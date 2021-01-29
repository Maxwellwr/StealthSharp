#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ISerializationResult.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Serialization
{
    public interface ISerializationResult : IDisposable
    {
        Memory<byte> Memory { get; }
    }
}