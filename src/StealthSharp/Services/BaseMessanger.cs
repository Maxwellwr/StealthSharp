#region Copyright

// -----------------------------------------------------------------------
// <copyright file="BaseMessanger.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Services
{
    // public abstract class BaseMessanger : BaseService, IMessangerBase
    // {
    //     public BaseMessanger(IStealthClient client, IEventSystemService eventSystemService)
    //         : base(client)
    //     {
    //         eventSystemService.MessangerIncomingText += EventService_MessangerIncomingText;
    //     }
    //
    //     public event EventHandler Connected;
    //
    //     public event EventHandler Disconnected;
    //
    //     public event EventHandler<MessangerTextEventArgs> IncomingMessage;
    //
    //     public event EventHandler<MessangerErrorEventArgs> Error;
    //
    //     public bool IsConnected
    //     {
    //         get => await Client.SendPacketAsync<bool>(PacketType.SCMessenger_GetConnected, MessangerType);
    //         private set => await Client.SendPacketAsync(PacketType.SCMessenger_SetConnected, MessangerType, value);
    //     }
    //
    //     public string Token
    //     {
    //         get => await Client.SendPacketAsync<string>(PacketType.SCMessenger_GetToken, MessangerType);
    //         private set => await Client.SendPacketAsync(PacketType.SCMessenger_SetToken, MessangerType, value);
    //     }
    //
    //     public string Name
    //     {
    //         get => await Client.SendPacketAsync<string>(PacketType.SCMessenger_GetName, MessangerType);
    //     }
    //
    //     protected abstract MessangerType MessangerType { get; }
    //
    //     public void Connect(string token)
    //     {
    //         if (IsConnected)
    //         {
    //             IsConnected = false;
    //         }
    //
    //         Token = token;
    //         IsConnected = true;
    //     }
    //
    //     public void SendMessage(string message, string recieverId)
    //     {
    //         await Client.SendPacketAsync(PacketType.SCMessenger_SendMessage, MessangerType, message, recieverId);
    //     }
    //
    //     private void EventService_MessangerIncomingText(object sender, MessangerIncomingTextEventArgs e)
    //     {
    //         if (e.MessangerType != MessangerType)
    //         {
    //             return;
    //         }
    //
    //         switch (e.EventCode)
    //         {
    //             case MessangerEventType.Connected: // Connected
    //                 Connected?.Invoke(this, new EventArgs());
    //                 break;
    //             case MessangerEventType.Disconnected: // Disconnected
    //                 Disconnected?.Invoke(this, new EventArgs());
    //                 break;
    //             case MessangerEventType.Message: // Incoming Message
    //                 IncomingMessage?.Invoke(this, e as MessangerTextEventArgs);
    //                 break;
    //             case MessangerEventType.Error: // Error
    //                 Error?.Invoke(this, e as MessangerErrorEventArgs);
    //                 break;
    //         }
    //     }
    // }
}