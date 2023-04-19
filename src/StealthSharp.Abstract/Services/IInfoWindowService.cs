#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IInfoWindowService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;

#endregion

namespace StealthSharp.Services
{
    public interface IInfoWindowService
    {
        Task ClearInfoWindowAsync();

        Task FillInfoWindowAsync(string s);
    }
}