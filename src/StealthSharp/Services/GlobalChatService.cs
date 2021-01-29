#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="GlobalChatService.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;
using StealthSharp.Network;

namespace StealthSharp.Services
{
    public class GlobalChatService:BaseService, IGlobalChatService
    {
        public GlobalChatService(
            IStealthSharpClient<ushort, uint, ushort> client)
            : base(client)
        {
        }
        
        public Task GlobalChatJoinChannelAsync(string chatName  )
        {
            return Client.SendPacketAsync(PacketType.SCGlobalChatJoinChannel, chatName);
        }

        public Task GlobalChatLeaveChannelAsync(   ){
            return Client.SendPacketAsync(PacketType.SCGlobalChatLeaveChannel);
            
        }

        public Task GlobalChatSendMsgAsync(string msgText  ){
        
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