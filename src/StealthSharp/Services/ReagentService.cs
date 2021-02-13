#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ReagentService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class ReagentService : BaseService, IReagentService
    {
        private readonly IObjectSearchService _objectSearchService;

        public ReagentService(IStealthSharpClient client,
            IObjectSearchService objectSearchService)
            : base(client)
        {
            _objectSearchService = objectSearchService;
        }

        public async Task<int> GetBMCountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.BM, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetBPCountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.BP, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetGACountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.GA, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetGSCountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.GS, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetMRCountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.MR, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetNSCountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.NS, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetSACountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.SA, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }

        public async Task<int> GetSSCountAsync()
        {
            await _objectSearchService.FindTypeExAsync((ushort) Reagents.SS, 0x0000,
                await _objectSearchService.GetBackpackAsync(), true);
            return await _objectSearchService.GetFindFullQuantityAsync();
        }
    }
}