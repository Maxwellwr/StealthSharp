#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ISystemService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface ISystemService
    {
        void SetSilentMode(bool value);
        
        Task<bool> GetSilentModeAsync();

        void Alarm();

        void ConsoleEntryReply(string text);

        void ConsoleEntryUnicodeReply(string text);

        void HelpRequest();

        void QuestRequest();

        void RequestStats(uint objID);

        Task<bool> ClientHide(uint id);
    }
}