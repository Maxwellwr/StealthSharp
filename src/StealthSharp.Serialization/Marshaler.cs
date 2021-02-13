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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;

namespace StealthSharp.Serialization
{
    public class Marshaler: IMarshaler
    {
        private readonly IReflectionCache _reflectionCache;
        private readonly SerializationOptions _serializationOptions;
        private readonly ICustomConverterFactory? _customConverterFactory;
        
        public Marshaler(IReflectionCache? reflectionCache, 
            IOptions<SerializationOptions>? serializationOptions,
            ICustomConverterFactory? customConverterFactory)
        {
            _reflectionCache = reflectionCache ?? throw new ArgumentNullException(nameof(reflectionCache));
            _customConverterFactory =
                customConverterFactory ?? throw new ArgumentNullException(nameof(customConverterFactory));
            _serializationOptions = serializationOptions?.Value ?? new SerializationOptions();
        }
        
        public int SizeOf(object? element)
        {
            if (element is null)
            {
                return 0;
            }
            else if (element is string s)
            {
                return SizeOf(_serializationOptions.StringSizeType) + s.Length * 2;
            }
            else if (element is IList array)
            {
                var itemsType = element.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.Name.StartsWith("IList"))?
                    .GenericTypeArguments
                    .FirstOrDefault();
                if (itemsType == null)
                    throw SerializationException.CollectionItemTypeNotFoundException(element.GetType().Name);

                return Enumerable.Range(0, array.Count)
                    .Sum(idx => SizeOf(array[idx])) + SizeOf(_serializationOptions.ArrayCountType);
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
                if (reflectionMetadata != null)
                    return  reflectionMetadata.Properties.Sum(p => SizeOf(p.Get(element)));

                if (_customConverterFactory != null &&
                    _customConverterFactory.TryGetConverter(element.GetType(), out var converter))
                {
                    return converter!.SizeOf(element);
                }
                
                return SizeOf(element.GetType());
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

    public interface IMarshaler
    {
        int SizeOf(object? element);

        /// <summary>
        /// Size of simple types like byte, int, short or Enum underlying type
        /// </summary>
        /// <param name="type">Simple type</param>
        /// <returns>Size in bytes</returns>
        int SizeOf(Type type);
    }
}