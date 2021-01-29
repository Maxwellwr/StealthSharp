#region Copyright

// -----------------------------------------------------------------------
// <copyright file="BitConvert.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Options;

namespace StealthSharp.Serialization
{
    public class BitConvert : IBitConvert
    {
        private readonly IReflectionCache _reflectionCache;

        public BitConvert(IReflectionCache reflectionCache)
        {
            _reflectionCache = reflectionCache;
        }
        
        public int SizeOf(object? element)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            else if (element is string s)
            {
                return SizeOf(typeof(int)) + s.Length * 2;
            }
            else if (element is IList array)
            {
                var underlineType = element.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.Name.StartsWith("IList"))?
                    .GenericTypeArguments
                    .FirstOrDefault();
                if (underlineType == null)
                    throw SerializationException.ConverterNotFoundType(
                        $"Underline type of collection {element.GetType()} not found");

                return Enumerable.Range(0, array.Count)
                    .Sum(idx => SizeOf(array[idx])) + SizeOf(typeof(int));
            }
            else if (element is ITuple tuple)
            {
                var size = 0;
                for (var i = 0; i < tuple.Length; i++)
                {
                    size += SizeOf(tuple[i]);
                }

                return size;
            }
            else
            {
                var reflectionMetadata = _reflectionCache.GetMetadata(element.GetType());
                if (reflectionMetadata == null)
                    return SizeOf(element.GetType());
                
                var bodySize = reflectionMetadata.Contains(PacketDataType.Body)
                    ? SizeOf( reflectionMetadata[PacketDataType.Body].Get(element))
                    : 0;

                var length = bodySize
                             + reflectionMetadata.MetaLength
                             + reflectionMetadata.IdLength
                             + reflectionMetadata.TypeMapperLength;

                if (reflectionMetadata.Contains(PacketDataType.Dynamic))
                {
                    foreach (var property in reflectionMetadata[PacketDataType.Dynamic].PropertyType.GetProperties())
                    {
                        length += SizeOf(property.GetValue(reflectionMetadata[PacketDataType.Dynamic].Get(element)));
                    }
                }

                return length;
            }
        }

        /// <summary>
        /// Size of simple types like byte, int, short or Enum underlying type
        /// </summary>
        /// <param name="type">Simple type</param>
        /// <returns>Size in bytes</returns>
        public int SizeOf(Type type)
        {
            if (type == typeof(bool))
            {
                return SizeOf(typeof(byte));
            }
            else if (type.IsEnum)
            {
                return SizeOf(type.GetEnumUnderlyingType());
            }
            else
            {
                return Marshal.SizeOf(type);
            }
        }
    }
}