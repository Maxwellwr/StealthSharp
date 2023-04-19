#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IReflectionCache.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Serialization
{
    public interface IReflectionCache
    {
        IReflectionMetadata? GetMetadata<T>()
        {
            return GetMetadata(typeof(T));
        }

        IReflectionMetadata? GetMetadata(Type type);
    }
}