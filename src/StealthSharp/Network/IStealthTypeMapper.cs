#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="IStealthTypeMapper.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using System.Threading.Tasks;

namespace StealthSharp.Network
{
    public interface IStealthTypeMapper<TMapping, TId> : ITypeMapper<TMapping, TId>
    {
        Task SetMappedTypeAsync(TId request, Type? responseType);
    }
}