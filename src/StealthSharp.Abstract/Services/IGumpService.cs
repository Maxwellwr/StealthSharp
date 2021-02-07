#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IGumpService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Model;

namespace StealthSharp.Services
{
    public interface IGumpService
    {
        Task<ushort> GetGumpsCountAsync();

        Task<bool> GetIsGumpAsync();

        Task AddGumpIgnoreByIDAsync(uint id);

        Task AddGumpIgnoreBySerialAsync(uint serial);

        Task ClearGumpsIgnoreAsync();

        Task CloseClientGumpAsync(uint id);

        Task CloseSimpleGumpAsync(ushort gumpIndex);

        Task<List<string>> GetGumpButtonsDescriptionAsync(ushort gumpIndex);

        Task<List<string>> GetGumpFullLinesAsync(ushort gumpIndex);

        Task<uint> GetGumpIDAsync(ushort gumpIndex);

        Task<GumpInfo> GetGumpInfoAsync(ushort gumpIndex);

        Task<uint> GetGumpSerialAsync(ushort gumpIndex);

        Task<List<string>> GetGumpShortLinesAsync(ushort gumpIndex);

        Task<List<string>> GetGumpTextLinesAsync(ushort gumpIndex);

        Task GumpAutoCheckBoxAsync(int checkboxId, int value);

        Task GumpAutoRadiobuttonAsync(int radiobuttonId, int value);

        Task GumpAutoTextEntryAsync(int textEntryId, string value);

        Task<bool> IsGumpCanBeClosedAsync(ushort gumpIndex);

        Task<bool> NumGumpButtonAsync(ushort gumpIndex, int value);

        Task<bool> NumGumpCheckBoxAsync(ushort gumpIndex, int checkboxId, int value);

        Task<bool> NumGumpRadiobuttonAsync(ushort gumpIndex, int radiobuttonId, int value);

        Task<bool> NumGumpTextEntryAsync(ushort gumpIndex, int textentryId, string value);

        Task WaitGumpAsync(string value);

        Task WaitGumpAsync(int value);

        Task WaitTextEntryAsync(string value);
    }
}