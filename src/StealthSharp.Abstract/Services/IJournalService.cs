#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IJournalService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IJournalService
    {
        Task<int> GetFoundedParamIdAsync();

        Task<int> GetHighJournalAsync();

        Task<string> GetLastJournalMessageAsync();

        Task<int> GetLineCountAsync();

        Task<uint> GetLineIdAsync();

        Task<int> GetLineIndexAsync();

        Task<byte> GetLineMsgTypeAsync();

        Task<string> GetLineNameAsync();

        Task<ushort> GetLineTextColorAsync();

        Task<ushort> GetLineTextFontAsync();

        Task<DateTime> GetLineTimeAsync();

        Task<ushort> GetLineTypeAsync();

        Task<int> GetLowJournalAsync();

        Task AddChatUserIgnoreAsync(string user);

        Task AddJournalIgnoreAsync(string str);

        Task AddToJournalAsync(string text);

        Task AddToSystemJournalAsync(string text);

        Task AddToSystemJournalAsync(string format, params object[] args);

        Task ClearChatUserIgnoreAsync();

        Task ClearJournalAsync();

        Task ClearJournalIgnoreAsync();

        Task ClearSystemJournalAsync();

        Task<int> InJournalAsync(string str);

        Task<int> InJournalBetweenTimesAsync(string str, DateTime timeBegin, DateTime timeEnd);

        Task<string> JournalAsync(int stringIndex);

        Task SetJournalLineAsync(int stringIndex, string text);

        Task<bool> WaitJournalLineAsync(DateTime startTime, string str, int maxWaitTimeMs = 0);

        Task<bool> WaitJournalLineSystemAsync(DateTime startTime, string str, int maxWaitTimeMs = 0);
    }
}