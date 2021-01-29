#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ICorrelation.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Network
{
    public interface ICorrelation<T> where T : unmanaged
    {
        T CorrelationId { get; set; }
    }
}