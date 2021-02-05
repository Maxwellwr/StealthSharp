#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="IGlobalChatService.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IGlobalChatService
    {
        Task GlobalChatJoinChannelAsync(string chatName);

        Task GlobalChatLeaveChannelAsync();

        Task GlobalChatSendMsgAsync(string msgText);

        Task<string> GlobalChatActiveChannel();

        Task<string[]> GlobalChatChannelsList();
    }
}