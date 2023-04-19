#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IStealthService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface IStealthService
    {
        Task<string> GetCurrentScriptPathAsync();

        Task<string> GetProfileNameAsync();

        Task<string> GetProfileShardNameAsync();

        Task<AboutData> GetStealthInfoAsync();

        Task<string> GetStealthPathAsync();

        Task<string> GetStealthProfilePathAsync();

        Task<string> GetShardNameAsync();

        Task<string> GetShardPathAsync();
    }
}