#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ReflectionCache.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace StealthSharp.Serialization
{
    public class ReflectionCache : IReflectionCache, IReflectionLevelCache
    {
        private readonly Dictionary<Type, IReflectionMetadata?> _dictionary = new();

        public IReflectionMetadata? GetMetadata(Type type) =>
            ((IReflectionLevelCache) this).GetMetadata(type, true);

        IReflectionMetadata? IReflectionLevelCache.GetMetadata(Type type, bool firstLevel)
        {
            if (!_dictionary.ContainsKey(type))
            {
                if (type
                    .GetProperties()
                    .SelectMany(p => p.GetCustomAttributes(false).OfType<PacketDataAttribute>())
                    .Any())

                    _dictionary[type] = new ReflectionMetadata(type, this, firstLevel);
                else
                    _dictionary[type] = null;
            }

            return _dictionary[type];
        }
    }
}