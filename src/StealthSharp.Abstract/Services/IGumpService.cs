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

        void AddGumpIgnoreByID(uint id);

        void AddGumpIgnoreBySerial(uint serial);

        void ClearGumpsIgnore();

        void CloseClientGump(uint id);

        void CloseSimpleGump(ushort gumpIndex);

        Task<List<string>> GetGumpButtonsDescriptionAsync(ushort gumpIndex);

        Task<List<string>> GetGumpFullLinesAsync(ushort gumpIndex);

        Task<uint> GetGumpIDAsync(ushort gumpIndex);

        Task<GumpInfo> GetGumpInfoAsync(ushort gumpIndex);

        Task<uint> GetGumpSerialAsync(ushort gumpIndex);

        Task<List<string>> GetGumpShortLinesAsync(ushort gumpIndex);

        Task<List<string>> GetGumpTextLinesAsync(ushort gumpIndex);

        void GumpAutoCheckBox(int checkboxId, int value);

        void GumpAutoRadiobutton(int radiobuttonId, int value);

        void GumpAutoTextEntry(int textEntryId, string value);

        Task<bool> IsGumpCanBeClosedAsync(ushort gumpIndex);

        Task<bool> NumGumpButtonAsync(ushort gumpIndex, int value);

        Task<bool> NumGumpCheckBoxAsync(ushort gumpIndex, int checkboxId, int value);

        Task<bool> NumGumpRadiobuttonAsync(ushort gumpIndex, int radiobuttonId, int value);

        Task<bool> NumGumpTextEntryAsync(ushort gumpIndex, int textentryId, string value);

        void WaitGump(string value);

        void WaitGump(int value);

        void WaitTextEntry(string value);
    }
}