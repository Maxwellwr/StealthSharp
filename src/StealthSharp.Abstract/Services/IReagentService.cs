#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IReagentService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IReagentService
    {
        Task<int> GetBMCountAsync();

        Task<int> GetBPCountAsync();

        Task<int> GetGACountAsync();

        Task<int> GetGSCountAsync();

        Task<int> GetMRCountAsync();

        Task<int> GetNSCountAsync();

        Task<int> GetSACountAsync();

        Task<int> GetSSCountAsync();
    }
}