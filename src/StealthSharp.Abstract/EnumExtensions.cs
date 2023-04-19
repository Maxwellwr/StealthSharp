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

        public static Type? GetEnumDataType(this Enum @enum)
        {
            var enumType = @enum.GetType();
            return enumType.GetMember(@enum.ToString())[0]
                .GetCustomAttribute<EventDataTypeAttribute>(false)?
                .DataType;
        }
    }
}