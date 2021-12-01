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
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class ReflectionCache : IReflectionCache
    {
        private readonly Dictionary<Type, IReflectionMetadata?> _dictionary = new();
        public IReflectionMetadata? GetMetadata(Type type)
        {
            if (_dictionary.ContainsKey(type)) 
                return _dictionary[type];
            
            if (type.GetCustomAttribute<SerializableAttribute>() is not null)
                _dictionary[type] = new ReflectionMetadata(type);
            else
                _dictionary[type] = null;
            
            return _dictionary[type];
        }
    }
}