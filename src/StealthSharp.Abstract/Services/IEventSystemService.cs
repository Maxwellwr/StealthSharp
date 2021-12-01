#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IEventSystemService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;
using StealthSharp.Enumeration;

namespace StealthSharp.Services
{
    public interface IEventSystemService
    {
        Task Subscribe<T>(EventType eventType, Action<T> action);
        Task Unsubscribe(EventType eventType, Delegate action);
    }
}