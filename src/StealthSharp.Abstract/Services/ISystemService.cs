﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ISystemService.cs" company="StealthSharp">
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
    public interface ISystemService
    {
        Task SetSilentModeAsync(bool value);

        Task<bool> GetSilentModeAsync();

        Task AlarmAsync();

        Task ConsoleEntryReplyAsync(string text);

        Task ConsoleEntryUnicodeReplyAsync(string text);

        Task HelpRequestAsync();

        Task QuestRequestAsync();

        Task RequestStatsAsync(uint objId);

        Task<bool> ClientHide(uint id);
    }
}