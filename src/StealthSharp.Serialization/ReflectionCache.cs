#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ReflectionCache.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Concurrent;
using System.Reflection;

#endregion

namespace StealthSharp.Serialization
{
    public class ReflectionCache : IReflectionCache
    {
        // Used from sending threads and from the receive loop at the same time.
        private readonly ConcurrentDictionary<Type, IReflectionMetadata?> _dictionary = new();

        public IReflectionMetadata? GetMetadata(Type type)
        {
            return _dictionary.GetOrAdd(type, static t =>
                t.GetCustomAttribute<SerializableAttribute>() is not null ? new ReflectionMetadata(t) : null);
        }
    }
}