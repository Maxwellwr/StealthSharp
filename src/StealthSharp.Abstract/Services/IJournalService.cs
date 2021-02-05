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
        Task<int> GetFoundedParamIDAsync();

        Task<int> GetHighJournalAsync();

        Task<string> GetLastJournalMessageAsync();

        Task<int> GetLineCountAsync();

        Task<uint> GetLineIDAsync();

        Task<int> GetLineIndexAsync();

        Task<byte> GetLineMsgTypeAsync();

        Task<string> GetLineNameAsync();

        Task<ushort> GetLineTextColorAsync();

        Task<ushort> GetLineTextFontAsync();

        Task<DateTime> GetLineTimeAsync();

        Task<ushort> GetLineTypeAsync();

        Task<int> GetLowJournalAsync();

        void AddChatUserIgnore(string user);

        void AddJournalIgnore(string str);

        void AddToJournal(string text);

        void AddToSystemJournal(string text);

        void AddToSystemJournal(string format, params object[] args);

        void ClearChatUserIgnore();

        void ClearJournal();

        void ClearJournalIgnore();

        void ClearSystemJournal();

        Task<int> InJournalAsync(string str);

        Task<int> InJournalBetweenTimesAsync(string str, DateTime timeBegin, DateTime timeEnd);

        Task<string> JournalAsync(int stringIndex);

        void SetJournalLine(int stringIndex, string text);

        Task<bool> WaitJournalLineAsync(DateTime startTime, string str, int maxWaitTimeMS = 0);

        Task<bool> WaitJournalLineSystemAsync(DateTime startTime, string str, int maxWaitTimeMS = 0);
    }
}