#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="EnumExtensions.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Concurrent;
using System.Reflection;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp
{
    public static class EnumExtensions
    {
        public static bool GetEnum<T>(this string name, out T result)
            where T : struct
        {
            return Enum.TryParse(name.Replace(" ", string.Empty), true, out result);
        }

        // Attribute lookup through reflection is done for every received event, so it is cached per enum value.
        private static readonly ConcurrentDictionary<Enum, Type?> EnumDataTypes = new();

        public static Type? GetEnumDataType(this Enum @enum)
        {
            return EnumDataTypes.GetOrAdd(@enum, static e => e.GetType().GetMember(e.ToString())[0]
                .GetCustomAttribute<EventDataTypeAttribute>(false)?
                .DataType);
        }
    }
}