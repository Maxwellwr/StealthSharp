#region Copyright

// -----------------------------------------------------------------------
// <copyright file="JournalService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class JournalService : BaseService, IJournalService
    {
        public JournalService(IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }

        public Task<int> GetFoundedParamIDAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetFoundedParamID);
        }

        public Task<int> GetHighJournalAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCHighJournal);
        }

        public Task<string> GetLastJournalMessageAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCLastJournalMessage);
        }

        public Task<int> GetLineCountAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetLineCount);
        }

        public Task<uint> GetLineIDAsync()
        {
            return Client.SendPacketAsync<uint>(PacketType.SCGetLineID);
        }

        public Task<int> GetLineIndexAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCGetLineIndex);
        }

        public Task<byte> GetLineMsgTypeAsync()
        {
            return Client.SendPacketAsync<byte>(PacketType.SCGetLineMsgType);
        }

        public Task<string> GetLineNameAsync()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGetLineName);
        }

        public Task<ushort> GetLineTextColorAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetLineTextColor);
        }

        public Task<ushort> GetLineTextFontAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetLineTextFont);
        }

        public Task<DateTime> GetLineTimeAsync()
        {
            return Client.SendPacketAsync<DateTime>(PacketType.SCGetLineTime);
        }

        public Task<ushort> GetLineTypeAsync()
        {
            return Client.SendPacketAsync<ushort>(PacketType.SCGetLineType);
        }

        public Task<int> GetLowJournalAsync()
        {
            return Client.SendPacketAsync<int>(PacketType.SCLowJournal);
        }

        public Task AddChatUserIgnoreAsync(string user)
        {
             return Client.SendPacketAsync(PacketType.SCUAddChatUserIgnore, user);
        }

        public Task AddJournalIgnoreAsync(string str)
        {
             return Client.SendPacketAsync(PacketType.SCAddJournalIgnore, str);
        }

        public Task AddToJournalAsync(string text)
        {
             return Client.SendPacketAsync(PacketType.SCAddToJournal, text);
        }

        public Task AddToSystemJournalAsync(string text)
        {
             return Client.SendPacketAsync(PacketType.SCAddToSystemJournal, text);
        }

        public Task AddToSystemJournalAsync(string format, params object[] args)
        {
            return AddToSystemJournalAsync(string.Format(format, args));
        }

        public Task ClearChatUserIgnoreAsync()
        {
             return Client.SendPacketAsync(PacketType.SCClearChatUserIgnore);
        }

        public Task ClearJournalAsync()
        {
             return Client.SendPacketAsync(PacketType.SCClearJournal);
        }

        public Task ClearJournalIgnoreAsync()
        {
             return Client.SendPacketAsync(PacketType.SCClearJournalIgnore);
        }

        public Task ClearSystemJournalAsync()
        {
             return Client.SendPacketAsync(PacketType.SCClearSystemJournal);
        }

        public Task<int> InJournalAsync(string str)
        {
            return Client.SendPacketAsync<string, int>(PacketType.SCInJournal, str);
        }

        public Task<int> InJournalBetweenTimesAsync(string str, DateTime timeBegin, DateTime timeEnd)
        {
            return Client.SendPacketAsync<(string, DateTime, DateTime), int>(PacketType.SCInJournalBetweenTimes, (str, timeBegin, timeEnd));
        }

        public Task<string> JournalAsync(int stringIndex)
        {
            return Client.SendPacketAsync<int, string>(PacketType.SCJournal, stringIndex);
        }

        public Task SetJournalLineAsync(int stringIndex, string text)
        {
             return Client.SendPacketAsync(PacketType.SCSetJournalLine, (stringIndex, text));
        }

        public async Task<bool> WaitJournalLineAsync(DateTime startTime, string str, int maxWaitTimeMS = 0)
        {
            var infinite = maxWaitTimeMS <= 0;
            var stopTime = startTime.AddMilliseconds(maxWaitTimeMS);

            do
            {
                if (await InJournalBetweenTimesAsync(str, startTime, infinite ? DateTime.Now : stopTime) >= 0)
                {
                    return true;
                }
            } while (infinite || (stopTime > DateTime.Now));

            return false;
        }

        public async Task<bool> WaitJournalLineSystemAsync(DateTime startTime, string str, int maxWaitTimeMS = 0)
        {
            var infinite = maxWaitTimeMS <= 0;
            var stopTime = startTime.AddMilliseconds(maxWaitTimeMS);

            do
            {
                if ((await InJournalBetweenTimesAsync(str, startTime, infinite ? DateTime.Now : stopTime) >= 0)
                    && (await GetLineNameAsync()).Equals("System"))
                {
                    return true;
                }
            } while (infinite || (stopTime > DateTime.Now));

            return false;
        }
    }
}