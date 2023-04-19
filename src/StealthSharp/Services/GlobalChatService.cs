#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="GlobalChatService.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public class GlobalChatService : BaseService, IGlobalChatService
    {
        public GlobalChatService(
            IStealthSharpClient client)
            : base(client)
        {
        }

        public Task GlobalChatJoinChannelAsync(string chatName)
        {
            return Client.SendPacketAsync(PacketType.SCGlobalChatJoinChannel, chatName);
        }

        public Task GlobalChatLeaveChannelAsync()
        {
            return Client.SendPacketAsync(PacketType.SCGlobalChatLeaveChannel);
        }

        public Task GlobalChatSendMsgAsync(string msgText)
        {
            return Client.SendPacketAsync(PacketType.SCGlobalChatSendMsg, msgText);
        }

        public Task<string> GlobalChatActiveChannel()
        {
            return Client.SendPacketAsync<string>(PacketType.SCGlobalChatActiveChannel);
        }

        public Task<string[]> GlobalChatChannelsList()
        {
            return Client.SendPacketAsync<string[]>(PacketType.SCGlobalChatChannelsList);
        }
    }
}