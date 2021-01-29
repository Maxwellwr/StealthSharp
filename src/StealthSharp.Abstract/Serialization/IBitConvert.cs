#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IBitConvert.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Serialization
{
    public interface IBitConvert
    {
        int SizeOf(object? element);
        int SizeOf(Type type);
    }
}