#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ITypeMapping.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Network
{
    public interface ITypeMapping<out T>
        where T : unmanaged
    {
        T TypeId { get; }
    }
}