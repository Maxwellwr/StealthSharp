#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ITypeMapper.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;

namespace StealthSharp.Network
{
    public interface ITypeMapper<TMapping, TId>
    {
        Task SetMappedTypeAsync(TId request, Type? responseType);
        Task<Type?> GetMappedTypeAsync(TMapping mappingId, TId correlationId);
    }
}