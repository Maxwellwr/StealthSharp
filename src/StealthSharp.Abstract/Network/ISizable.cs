#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ISizable.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Network
{
    public interface ISizable<out T> where T : unmanaged
    {
        T Length { get; }
    }
}