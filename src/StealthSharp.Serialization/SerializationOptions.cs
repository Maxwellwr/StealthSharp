#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializationOptions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Serialization
{
    public class SerializationOptions
    {
        public Type ArrayCountType { get; set; } = typeof(int);
        public Type StringSizeType { get; set; } = typeof(int);
    }
}