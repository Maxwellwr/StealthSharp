#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="ICustomConverterFactory.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;

namespace StealthSharp.Serialization
{
    public interface ICustomConverterFactory
    {
        bool TryGetConverter(Type propertyType, out ICustomConverter? customConverter);
    }
}