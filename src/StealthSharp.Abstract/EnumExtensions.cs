#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="EnumExtensions.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

namespace StealthSharp
{
    public static class EnumExtensions
    {
        public static bool GetEnum<T>(this string name, out T result)
            where T : struct
        {
            return System.Enum.TryParse<T>(name.Replace(" ", string.Empty), true, out result);
        }
    }
}