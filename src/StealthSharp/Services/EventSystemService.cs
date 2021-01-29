#region Copyright

// -----------------------------------------------------------------------
// <copyright file="EventSystemService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Services
{
    // [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter",
    //     Justification = "OK")]
    // public class EventSystemService : BaseService, IEventSystemService
    // {
    //     public EventSystemService(IStealthClient client)
    //         : 
    //     {
    //         client.ServerEventRecieve += cln_ServerEventRecieve;
    //     }
    //
    //     public event EventHandler<ItemEventArgs> ItemInfo
    //     {
    //         add
    //         {
    //             var handler = _itemInfo;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ItemInfo);
    //             }
    //
    //             _itemInfo += value;
    //         }
    //
    //         remove
    //         {
    //             _itemInfo -= value;
    //
    //             var handler = _itemInfo;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ItemInfo);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<ItemEventArgs> ItemDeleted
    //     {
    //         add
    //         {
    //             var handler = _itemDeleted;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ItemDeleted);
    //             }
    //
    //             _itemDeleted += value;
    //         }
    //
    //         remove
    //         {
    //             _itemDeleted -= value;
    //
    //             var handler = _itemDeleted;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ItemDeleted);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<SpeechEventArgs> Speech
    //     {
    //         add
    //         {
    //             var handler = _speech;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Speech);
    //             }
    //
    //             _speech += value;
    //         }
    //
    //         remove
    //         {
    //             _speech -= value;
    //
    //             var handler = _speech;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Speech);
    //             }
    //         }
    //     }
    //
    //     [Obsolete("Deprecated, use DrawObject instead")]
    //     public event EventHandler<ObjectEventArgs> DrawGamePlayer
    //     {
    //         add
    //         {
    //             var handler = _drawGamePlayer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.DrawGamePlayer);
    //             }
    //
    //             _drawGamePlayer += value;
    //         }
    //
    //         remove
    //         {
    //             _drawGamePlayer -= value;
    //
    //             var handler = _drawGamePlayer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.DrawGamePlayer);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<MoveRejectionEventArgs> MoveRejection
    //     {
    //         add
    //         {
    //             var handler = _moveRejection;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.MoveRejection);
    //             }
    //
    //             _moveRejection += value;
    //         }
    //
    //         remove
    //         {
    //             _moveRejection -= value;
    //
    //             var handler = _moveRejection;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.MoveRejection);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<DrawContainerEventArgs> DrawContainer
    //     {
    //         add
    //         {
    //             var handler = _drawContainer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.DrawContainer);
    //             }
    //
    //             _drawContainer += value;
    //         }
    //
    //         remove
    //         {
    //             _drawContainer -= value;
    //
    //             var handler = _drawContainer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.DrawContainer);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<AddItemToContainerEventArgs> AddItemToContainer
    //     {
    //         add
    //         {
    //             var handler = _addItemToContainer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.AddItemToContainer);
    //             }
    //
    //             _addItemToContainer += value;
    //         }
    //
    //         remove
    //         {
    //             _addItemToContainer -= value;
    //
    //             var handler = _addItemToContainer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.AddItemToContainer);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<AddMultipleItemsInContainerEventArgs> AddMultipleItemsInContainer
    //     {
    //         add
    //         {
    //             var handler = _addMultipleItemsInContainer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.AddMultipleItemsInCont);
    //             }
    //
    //             _addMultipleItemsInContainer += value;
    //         }
    //
    //         remove
    //         {
    //             _addMultipleItemsInContainer -= value;
    //
    //             var handler = _addMultipleItemsInContainer;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.AddMultipleItemsInCont);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<RejectMoveItemEventArgs> RejectMoveItem
    //     {
    //         add
    //         {
    //             var handler = _rejectMoveItem;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.RejectMoveItem);
    //             }
    //
    //             _rejectMoveItem += value;
    //         }
    //
    //         remove
    //         {
    //             _rejectMoveItem -= value;
    //
    //             var handler = _rejectMoveItem;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.RejectMoveItem);
    //             }
    //         }
    //     }
    //
    //     [Obsolete("Deprecated, use DrawObject instead")]
    //     public event EventHandler<ObjectEventArgs> UpdateChar
    //     {
    //         add
    //         {
    //             var handler = _updateChar;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.UpdateChar);
    //             }
    //
    //             _updateChar += value;
    //         }
    //
    //         remove
    //         {
    //             _updateChar -= value;
    //
    //             var handler = _updateChar;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.UpdateChar);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<ObjectEventArgs> DrawObject
    //     {
    //         add
    //         {
    //             var handler = _drawObject;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.DrawObject);
    //             }
    //
    //             _drawObject += value;
    //         }
    //
    //         remove
    //         {
    //             _drawObject -= value;
    //
    //             var handler = _drawObject;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.DrawObject);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<MenuEventArgs> Menu
    //     {
    //         add
    //         {
    //             var handler = _menu;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Menu);
    //             }
    //
    //             _menu += value;
    //         }
    //
    //         remove
    //         {
    //             _menu -= value;
    //
    //             var handler = _menu;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Menu);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<MapMessageEventArgs> MapMessage
    //     {
    //         add
    //         {
    //             var handler = _mapMessage;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.MapMessage);
    //             }
    //
    //             _mapMessage += value;
    //         }
    //
    //         remove
    //         {
    //             _mapMessage -= value;
    //
    //             var handler = _mapMessage;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.MapMessage);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<AllowRefuseAttackEventArgs> AllowRefuseAttack
    //     {
    //         add
    //         {
    //             var handler = _allowRefuseAttack;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Allow_RefuseAttack);
    //             }
    //
    //             _allowRefuseAttack += value;
    //         }
    //
    //         remove
    //         {
    //             _allowRefuseAttack -= value;
    //
    //             var handler = _allowRefuseAttack;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Allow_RefuseAttack);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<ClilocSpeechEventArgs> ClilocSpeech
    //     {
    //         add
    //         {
    //             var handler = _clilocSpeech;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ClilocSpeech);
    //             }
    //
    //             _clilocSpeech += value;
    //         }
    //
    //         remove
    //         {
    //             _clilocSpeech -= value;
    //
    //             var handler = _clilocSpeech;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ClilocSpeech);
    //             }
    //         }
    //     }
    //
    //     [Obsolete("Deprecated, use ClilocSpeech instead")]
    //     public event EventHandler<ClilocSpeechAffixEventArgs> ClilocSpeechAffix
    //     {
    //         add
    //         {
    //             var handler = _clilocSpeechAffix;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ClilocSpeechAffix);
    //             }
    //
    //             _clilocSpeechAffix += value;
    //         }
    //
    //         remove
    //         {
    //             _clilocSpeechAffix -= value;
    //
    //             var handler = _clilocSpeechAffix;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ClilocSpeechAffix);
    //             }
    //         }
    //     }
    //
    //     [Obsolete("Deprecated, use Speech instead")]
    //     public event EventHandler<UnicodeSpeechEventArgs> UnicodeSpeech
    //     {
    //         add
    //         {
    //             var handler = _unicodeSpeech;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.UnicodeSpeech);
    //             }
    //
    //             _unicodeSpeech += value;
    //         }
    //
    //         remove
    //         {
    //             _unicodeSpeech -= value;
    //
    //             var handler = _unicodeSpeech;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.UnicodeSpeech);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<Buff_DebuffSystemEventArgs> Buff_DebuffSystem
    //     {
    //         add
    //         {
    //             var handler = _buffDebuffSystem;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Buff_DebuffSystem);
    //             }
    //
    //             _buffDebuffSystem += value;
    //         }
    //
    //         remove
    //         {
    //             _buffDebuffSystem -= value;
    //
    //             var handler = _buffDebuffSystem;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Buff_DebuffSystem);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<EventArgs> ClientSendResync
    //     {
    //         add
    //         {
    //             var handler = _clientSendResync;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ClientSendResync);
    //             }
    //
    //             _clientSendResync += value;
    //         }
    //
    //         remove
    //         {
    //             _clientSendResync -= value;
    //
    //             var handler = _clientSendResync;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ClientSendResync);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<CharAnimationEventArgs> CharAnimation
    //     {
    //         add
    //         {
    //             var handler = _charAnimation;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.CharAnimation);
    //             }
    //
    //             _charAnimation += value;
    //         }
    //
    //         remove
    //         {
    //             _charAnimation -= value;
    //
    //             var handler = _charAnimation;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.CharAnimation);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<EventArgs> ICQDisconnect
    //     {
    //         add
    //         {
    //             var handler = _ICQDisconnect;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ICQDisconnect);
    //             }
    //
    //             _ICQDisconnect += value;
    //         }
    //
    //         remove
    //         {
    //             _ICQDisconnect -= value;
    //
    //             var handler = _ICQDisconnect;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ICQDisconnect);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<EventArgs> ICQConnect
    //     {
    //         add
    //         {
    //             var handler = _ICQConnect;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ICQConnect);
    //             }
    //
    //             _ICQConnect += value;
    //         }
    //
    //         remove
    //         {
    //             _ICQConnect -= value;
    //
    //             var handler = _ICQConnect;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ICQConnect);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<ICQIncomingTextEventArgs> ICQIncomingText
    //     {
    //         add
    //         {
    //             var handler = _ICQIncomingText;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ICQIncomingText);
    //             }
    //
    //             _ICQIncomingText += value;
    //         }
    //
    //         remove
    //         {
    //             _ICQIncomingText -= value;
    //
    //             var handler = _ICQIncomingText;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ICQIncomingText);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<ICQErrorEventArgs> ICQError
    //     {
    //         add
    //         {
    //             var handler = _ICQError;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.ICQError);
    //             }
    //
    //             _ICQError += value;
    //         }
    //
    //         remove
    //         {
    //             _ICQError -= value;
    //
    //             var handler = _ICQError;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.ICQError);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<IncomingGumpEventArgs> IncomingGump
    //     {
    //         add
    //         {
    //             var handler = _incomingGump;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.IncomingGump);
    //             }
    //
    //             _incomingGump += value;
    //         }
    //
    //         remove
    //         {
    //             _incomingGump -= value;
    //
    //             var handler = _incomingGump;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.IncomingGump);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<EventArgs> Timer1
    //     {
    //         add
    //         {
    //             var handler = _timer1;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Timer1);
    //             }
    //
    //             _timer1 += value;
    //         }
    //
    //         remove
    //         {
    //             _timer1 -= value;
    //
    //             var handler = _timer1;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Timer1);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<EventArgs> Timer2
    //     {
    //         add
    //         {
    //             var handler = _timer2;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Timer2);
    //             }
    //
    //             _timer2 += value;
    //         }
    //
    //         remove
    //         {
    //             _timer2 -= value;
    //
    //             var handler = _timer2;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Timer2);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<WindowsMessageEventArgs> WindowsMessage
    //     {
    //         add
    //         {
    //             var handler = _windowsMessage;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.WindowsMessage);
    //             }
    //
    //             _windowsMessage += value;
    //         }
    //
    //         remove
    //         {
    //             _windowsMessage -= value;
    //
    //             var handler = _windowsMessage;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.WindowsMessage);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<SoundEventArgs> Sound
    //     {
    //         add
    //         {
    //             var handler = _sound;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Sound);
    //             }
    //
    //             _sound += value;
    //         }
    //
    //         remove
    //         {
    //             _sound -= value;
    //
    //             var handler = _sound;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Sound);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<DeathEventArgs> Death
    //     {
    //         add
    //         {
    //             var handler = _death;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.Death);
    //             }
    //
    //             _death += value;
    //         }
    //
    //         remove
    //         {
    //             _death -= value;
    //
    //             var handler = _death;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.Death);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<QuestArrowEventArgs> QuestArrow
    //     {
    //         add
    //         {
    //             var handler = _questArrow;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.QuestArrow);
    //             }
    //
    //             _questArrow += value;
    //         }
    //
    //         remove
    //         {
    //             _questArrow -= value;
    //
    //             var handler = _questArrow;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.QuestArrow);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<PartyInviteEventArgs> PartyInvite
    //     {
    //         add
    //         {
    //             var handler = _partyInvite;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.PartyInvite);
    //             }
    //
    //             _partyInvite += value;
    //         }
    //
    //         remove
    //         {
    //             _partyInvite -= value;
    //
    //             var handler = _partyInvite;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.PartyInvite);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<MapPinEventArgs> MapPin
    //     {
    //         add
    //         {
    //             var handler = _mapPin;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.MapPin);
    //             }
    //
    //             _mapPin += value;
    //         }
    //
    //         remove
    //         {
    //             _mapPin -= value;
    //
    //             var handler = _mapPin;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.MapPin);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<GumpTextEntryEventArgs> GumpTextEntry
    //     {
    //         add
    //         {
    //             var handler = _gumpTextEntry;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.GumpTextEntry);
    //             }
    //
    //             _gumpTextEntry += value;
    //         }
    //
    //         remove
    //         {
    //             _gumpTextEntry -= value;
    //
    //             var handler = _gumpTextEntry;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.GumpTextEntry);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<GraphicalEffectEventArgs> GraphicalEffect
    //     {
    //         add
    //         {
    //             var handler = _graphicalEffect;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.GraphicalEffect);
    //             }
    //
    //             _graphicalEffect += value;
    //         }
    //
    //         remove
    //         {
    //             _graphicalEffect -= value;
    //
    //             var handler = _graphicalEffect;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.GraphicalEffect);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<IRCIncomingTextEventArgs> IRCIncomingText
    //     {
    //         add
    //         {
    //             var handler = _IRCIncomingText;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.IRCIncomingText);
    //             }
    //
    //             _IRCIncomingText += value;
    //         }
    //
    //         remove
    //         {
    //             _IRCIncomingText -= value;
    //
    //             var handler = _IRCIncomingText;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.IRCIncomingText);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<MessangerIncomingTextEventArgs> MessangerIncomingText
    //     {
    //         add
    //         {
    //             var handler = _messangerIncomingText;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.MessengerEvent);
    //             }
    //
    //             _messangerIncomingText += value;
    //         }
    //
    //         remove
    //         {
    //             _messangerIncomingText -= value;
    //
    //             var handler = _messangerIncomingText;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.MessengerEvent);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<SetGlobalVarEventArgs> SetGlobalVar
    //     {
    //         add
    //         {
    //             var handler = _setGlobalVar;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.SetGlobalVar);
    //             }
    //
    //             _setGlobalVar += value;
    //         }
    //
    //         remove
    //         {
    //             _setGlobalVar -= value;
    //
    //             var handler = _setGlobalVar;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.SetGlobalVar);
    //             }
    //         }
    //     }
    //
    //     public event EventHandler<UpdateObjectStatsEventArgs> UpdateObjectStats
    //     {
    //         add
    //         {
    //             var handler = _updateObjectStats;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCSetEventProc, EventTypes.UpdateObjStats);
    //             }
    //
    //             _updateObjectStats += value;
    //         }
    //
    //         remove
    //         {
    //             _updateObjectStats -= value;
    //
    //             var handler = _updateObjectStats;
    //             if (handler == null)
    //             {
    //                 await Client.SendPacketAsync(PacketType.SCClearEventProc, EventTypes.UpdateObjStats);
    //             }
    //         }
    //     }
    //
    //     private event EventHandler<ItemEventArgs> _itemInfo;
    //
    //     private event EventHandler<ItemEventArgs> _itemDeleted;
    //
    //     private event EventHandler<SpeechEventArgs> _speech;
    //
    //     private event EventHandler<ObjectEventArgs> _drawGamePlayer;
    //
    //     private event EventHandler<MoveRejectionEventArgs> _moveRejection;
    //
    //     private event EventHandler<DrawContainerEventArgs> _drawContainer;
    //
    //     private event EventHandler<AddItemToContainerEventArgs> _addItemToContainer;
    //
    //     private event EventHandler<AddMultipleItemsInContainerEventArgs> _addMultipleItemsInContainer;
    //
    //     private event EventHandler<RejectMoveItemEventArgs> _rejectMoveItem;
    //
    //     private event EventHandler<ObjectEventArgs> _updateChar;
    //
    //     private event EventHandler<ObjectEventArgs> _drawObject;
    //
    //     private event EventHandler<MenuEventArgs> _menu;
    //
    //     private event EventHandler<MapMessageEventArgs> _mapMessage;
    //
    //     private event EventHandler<AllowRefuseAttackEventArgs> _allowRefuseAttack;
    //
    //     private event EventHandler<ClilocSpeechEventArgs> _clilocSpeech;
    //
    //     private event EventHandler<ClilocSpeechAffixEventArgs> _clilocSpeechAffix;
    //
    //     private event EventHandler<UnicodeSpeechEventArgs> _unicodeSpeech;
    //
    //     private event EventHandler<Buff_DebuffSystemEventArgs> _buffDebuffSystem;
    //
    //     private event EventHandler<EventArgs> _clientSendResync;
    //
    //     private event EventHandler<CharAnimationEventArgs> _charAnimation;
    //
    //     private event EventHandler<EventArgs> _ICQDisconnect;
    //
    //     private event EventHandler<EventArgs> _ICQConnect;
    //
    //     private event EventHandler<ICQIncomingTextEventArgs> _ICQIncomingText;
    //
    //     private event EventHandler<ICQErrorEventArgs> _ICQError;
    //
    //     private event EventHandler<IncomingGumpEventArgs> _incomingGump;
    //
    //     private event EventHandler<EventArgs> _timer1;
    //
    //     private event EventHandler<EventArgs> _timer2;
    //
    //     private event EventHandler<WindowsMessageEventArgs> _windowsMessage;
    //
    //     private event EventHandler<SoundEventArgs> _sound;
    //
    //     private event EventHandler<DeathEventArgs> _death;
    //
    //     private event EventHandler<QuestArrowEventArgs> _questArrow;
    //
    //     private event EventHandler<PartyInviteEventArgs> _partyInvite;
    //
    //     private event EventHandler<MapPinEventArgs> _mapPin;
    //
    //     private event EventHandler<GumpTextEntryEventArgs> _gumpTextEntry;
    //
    //     private event EventHandler<GraphicalEffectEventArgs> _graphicalEffect;
    //
    //     private event EventHandler<IRCIncomingTextEventArgs> _IRCIncomingText;
    //
    //     private event EventHandler<MessangerIncomingTextEventArgs> _messangerIncomingText;
    //
    //     private event EventHandler<SetGlobalVarEventArgs> _setGlobalVar;
    //
    //     private event EventHandler<UpdateObjectStatsEventArgs> _updateObjectStats;
    //
    //     private void cln_ServerEventRecieve(object sender, ServerEventArgs e)
    //     {
    //         ProcessEvent(e.Data);
    //     }
    //
    //     private void ProcessEvent(ExecEventProcData data)
    //     {
    //         switch (data.EventType)
    //         {
    //             case EventTypes.ItemInfo:
    //                 OnItemInfo((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.ItemDeleted:
    //                 OnItemDeleted((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.Speech:
    //                 OnSpeech((string) data.Parameters[0], (string) data.Parameters[1], (uint) data.Parameters[2]);
    //                 break;
    //             case EventTypes.DrawGamePlayer:
    //                 OnDrawGamePlayer((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.MoveRejection:
    //                 OnMoveRejection(
    //                     (ushort) data.Parameters[0],
    //                     (ushort) data.Parameters[1],
    //                     (byte) data.Parameters[2],
    //                     (ushort) data.Parameters[3],
    //                     (ushort) data.Parameters[4]);
    //                 break;
    //             case EventTypes.DrawContainer:
    //                 OnDrawContainer((uint) data.Parameters[0], (ushort) data.Parameters[1]);
    //                 break;
    //             case EventTypes.AddItemToContainer:
    //                 OnAddItemToContainer((uint) data.Parameters[0], (uint) data.Parameters[1]);
    //                 break;
    //             case EventTypes.AddMultipleItemsInCont:
    //                 OnAddMultipleItemsInContainer((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.RejectMoveItem:
    //                 OnRejectMoveItem((RejectMoveItemReason) data.Parameters[0]);
    //                 break;
    //             case EventTypes.UpdateChar:
    //                 OnUpdateChar((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.DrawObject:
    //                 OnDrawObject((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.Menu:
    //                 OnMenu((uint) data.Parameters[0], (ushort) data.Parameters[1]);
    //                 break;
    //             case EventTypes.MapMessage:
    //                 OnMapMessage((uint) data.Parameters[0], (uint) data.Parameters[1], (uint) data.Parameters[2]);
    //                 break;
    //             case EventTypes.Allow_RefuseAttack:
    //                 OnAllowRefuseAttack((uint) data.Parameters[0], Convert.ToBoolean(data.Parameters[1]));
    //                 break;
    //             case EventTypes.ClilocSpeech:
    //                 OnClilocSpeech((uint) data.Parameters[0], (string) data.Parameters[1], (uint) data.Parameters[2],
    //                     (string) data.Parameters[3]);
    //                 break;
    //             case EventTypes.ClilocSpeechAffix:
    //                 OnClilocSpeechAffix(
    //                     (uint) data.Parameters[0],
    //                     (string) data.Parameters[1],
    //                     (uint) data.Parameters[2],
    //                     (string) data.Parameters[3],
    //                     (string) data.Parameters[4]);
    //                 break;
    //             case EventTypes.UnicodeSpeech:
    //                 OnUnicodeSpeech((string) data.Parameters[0], (string) data.Parameters[1],
    //                     (uint) data.Parameters[2]);
    //                 break;
    //             case EventTypes.Buff_DebuffSystem:
    //                 OnBuff_DebuffSystem((uint) data.Parameters[0], (ushort) data.Parameters[1],
    //                     (bool) data.Parameters[2]);
    //                 break;
    //             case EventTypes.ClientSendResync:
    //                 OnClientSendResync();
    //                 break;
    //             case EventTypes.CharAnimation:
    //                 OnCharAnimation((uint) data.Parameters[0], (ushort) data.Parameters[1]);
    //                 break;
    //             case EventTypes.ICQDisconnect:
    //                 OnICQDisconnect();
    //                 break;
    //             case EventTypes.ICQConnect:
    //                 OnICQConnect();
    //                 break;
    //             case EventTypes.ICQIncomingText:
    //                 OnICQIncomingText((uint) data.Parameters[0], (string) data.Parameters[1]);
    //                 break;
    //             case EventTypes.ICQError:
    //                 OnICQError((string) data.Parameters[0]);
    //                 break;
    //             case EventTypes.IncomingGump:
    //                 OnIncomingGump((uint) data.Parameters[0], (uint) data.Parameters[1], (int) data.Parameters[2],
    //                     (int) data.Parameters[3]);
    //                 break;
    //             case EventTypes.Timer1:
    //                 OnTimer1();
    //                 break;
    //             case EventTypes.Timer2:
    //                 OnTimer2();
    //                 break;
    //             case EventTypes.WindowsMessage:
    //                 OnWindowsMessage((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.Sound:
    //                 OnSound((ushort) data.Parameters[0], (ushort) data.Parameters[1], (ushort) data.Parameters[2],
    //                     (short) data.Parameters[3]);
    //                 break;
    //             case EventTypes.Death:
    //                 OnDeath(Convert.ToBoolean(data.Parameters[0]));
    //                 break;
    //             case EventTypes.QuestArrow:
    //                 OnQuestArrow((ushort) data.Parameters[0], (ushort) data.Parameters[1],
    //                     Convert.ToBoolean(data.Parameters[2]));
    //                 break;
    //             case EventTypes.PartyInvite:
    //                 OnPartyInvite((uint) data.Parameters[0]);
    //                 break;
    //             case EventTypes.MapPin:
    //                 OnMapPin((uint) data.Parameters[0], (byte) data.Parameters[1], (byte) data.Parameters[2],
    //                     (ushort) data.Parameters[3], (ushort) data.Parameters[4]);
    //                 break;
    //             case EventTypes.GumpTextEntry:
    //                 OnGumpTextEntry((uint) data.Parameters[0], (string) data.Parameters[1], (byte) data.Parameters[2],
    //                     (uint) data.Parameters[3], (string) data.Parameters[4]);
    //                 break;
    //             case EventTypes.GraphicalEffect:
    //                 OnGraphicalEffect(
    //                     (uint) data.Parameters[0],
    //                     (ushort) data.Parameters[1],
    //                     (ushort) data.Parameters[2],
    //                     (short) data.Parameters[3],
    //                     (uint) data.Parameters[4],
    //                     (ushort) data.Parameters[5],
    //                     (ushort) data.Parameters[6],
    //                     (short) data.Parameters[7],
    //                     (byte) data.Parameters[8],
    //                     (ushort) data.Parameters[9],
    //                     (byte) data.Parameters[10]);
    //                 break;
    //             case EventTypes.IRCIncomingText:
    //                 OnIRCIncomingText((string) data.Parameters[0]);
    //                 break;
    //             case EventTypes.MessengerEvent:
    //                 OnMessageIncomingText(
    //                     (MessangerType) data.Parameters[0],
    //                     (string) data.Parameters[1],
    //                     (string) data.Parameters[2],
    //                     (string) data.Parameters[3],
    //                     (string) data.Parameters[4],
    //                     (MessangerEventType) data.Parameters[5]);
    //                 break;
    //             case EventTypes.SetGlobalVar:
    //                 OnSetGlobalVar((string) data.Parameters[0], (string) data.Parameters[1]);
    //                 break;
    //             case EventTypes.UpdateObjStats:
    //                 OnUpdateObjectStats(
    //                     (uint) data.Parameters[0],
    //                     (uint) data.Parameters[1],
    //                     (uint) data.Parameters[2],
    //                     (uint) data.Parameters[3],
    //                     (uint) data.Parameters[4],
    //                     (uint) data.Parameters[5],
    //                     (uint) data.Parameters[6]);
    //                 break;
    //         }
    //     }
    //
    //     private void OnUpdateObjectStats(uint objectId, uint currentLife, uint maxLife, uint currentMana, uint maxMana,
    //         uint currentStamina, uint maxStamina)
    //     {
    //         _updateObjectStats?.Invoke(this,
    //             new UpdateObjectStatsEventArgs(objectId, currentLife, maxLife, currentMana, maxMana, currentStamina,
    //                 maxStamina));
    //     }
    //
    //     private void OnSetGlobalVar(string name, string value)
    //     {
    //         _setGlobalVar?.Invoke(this, new SetGlobalVarEventArgs(name, value));
    //     }
    //
    //     private void OnMessageIncomingText(MessangerType messangerType, string senderNickname, string senderId,
    //         string chatId, string eventMsg, MessangerEventType eventCode)
    //     {
    //         _messangerIncomingText?.Invoke(this,
    //             new MessangerIncomingTextEventArgs(messangerType, senderNickname, senderId, chatId, eventMsg,
    //                 eventCode));
    //     }
    //
    //     private void OnIRCIncomingText(string message)
    //     {
    //         _IRCIncomingText?.Invoke(this, new IRCIncomingTextEventArgs(message));
    //     }
    //
    //     private void OnGraphicalEffect(uint srcId, ushort srcX, ushort srcY, short srcZ, uint dstId, ushort dstX,
    //         ushort dstY, short dstZ, byte type, ushort itemId, byte fixedDir)
    //     {
    //         _graphicalEffect?.Invoke(this,
    //             new GraphicalEffectEventArgs(srcId, srcX, srcY, srcZ, dstId, dstX, dstY, dstZ, type, itemId, fixedDir));
    //     }
    //
    //     private void OnGumpTextEntry(uint gumpTextEntryId, string title, byte inputStyle, uint maxValue, string title2)
    //     {
    //         _gumpTextEntry?.Invoke(this,
    //             new GumpTextEntryEventArgs(gumpTextEntryId, title, inputStyle, maxValue, title2));
    //     }
    //
    //     private void OnMapPin(uint id, byte action, byte pinId, ushort x, ushort y)
    //     {
    //         _mapPin?.Invoke(this, new MapPinEventArgs(id, action, pinId, x, y));
    //     }
    //
    //     private void OnPartyInvite(uint inviterId)
    //     {
    //         _partyInvite?.Invoke(this, new PartyInviteEventArgs(inviterId));
    //     }
    //
    //     private void OnQuestArrow(ushort x, ushort y, bool isActive)
    //     {
    //         _questArrow?.Invoke(this, new QuestArrowEventArgs(x, y, isActive));
    //     }
    //
    //     private void OnDeath(bool isDead)
    //     {
    //         _death?.Invoke(this, new DeathEventArgs(isDead));
    //     }
    //
    //     private void OnSound(ushort soundId, ushort x, ushort y, short z)
    //     {
    //         _sound?.Invoke(this, new SoundEventArgs(soundId, x, y, z));
    //     }
    //
    //     private void OnWindowsMessage(uint lParam)
    //     {
    //         _windowsMessage?.Invoke(this, new WindowsMessageEventArgs(lParam));
    //     }
    //
    //     private void OnTimer2()
    //     {
    //         _timer2?.Invoke(this, new EventArgs());
    //     }
    //
    //     private void OnTimer1()
    //     {
    //         _timer1?.Invoke(this, new EventArgs());
    //     }
    //
    //     private void OnIncomingGump(uint serial, uint gumpId, int x, int y)
    //     {
    //         _incomingGump?.Invoke(this, new IncomingGumpEventArgs(serial, gumpId, x, y));
    //     }
    //
    //     private void OnICQError(string text)
    //     {
    //         _ICQError?.Invoke(this, new ICQErrorEventArgs(text));
    //     }
    //
    //     private void OnICQIncomingText(uint uin, string text)
    //     {
    //         _ICQIncomingText?.Invoke(this, new ICQIncomingTextEventArgs(uin, text));
    //     }
    //
    //     private void OnICQConnect()
    //     {
    //         _ICQConnect?.Invoke(this, new EventArgs());
    //     }
    //
    //     private void OnICQDisconnect()
    //     {
    //         _ICQDisconnect?.Invoke(this, new EventArgs());
    //     }
    //
    //     private void OnCharAnimation(uint objectId, ushort action)
    //     {
    //         _charAnimation?.Invoke(this, new CharAnimationEventArgs(objectId, action));
    //     }
    //
    //     private void OnClientSendResync()
    //     {
    //         _clientSendResync?.Invoke(this, new EventArgs());
    //     }
    //
    //     private void OnBuff_DebuffSystem(uint objectId, ushort attributeId, bool isEnabled)
    //     {
    //         _buffDebuffSystem?.Invoke(this, new Buff_DebuffSystemEventArgs(objectId, attributeId, isEnabled));
    //     }
    //
    //     private void OnUnicodeSpeech(string text, string senderName, uint senderId)
    //     {
    //         _unicodeSpeech?.Invoke(this, new UnicodeSpeechEventArgs(text, senderName, senderId));
    //     }
    //
    //     private void OnClilocSpeechAffix(uint senderId, string senderName, uint clilocId, string affix, string text)
    //     {
    //         _clilocSpeechAffix?.Invoke(this,
    //             new ClilocSpeechAffixEventArgs(senderId, senderName, clilocId, affix, text));
    //     }
    //
    //     private void OnClilocSpeech(uint senderId, string senderName, uint clilocId, string text)
    //     {
    //         _clilocSpeech?.Invoke(this, new ClilocSpeechEventArgs(senderId, senderName, clilocId, text));
    //     }
    //
    //     private void OnAllowRefuseAttack(uint targetId, bool isAttackOk)
    //     {
    //         _allowRefuseAttack?.Invoke(this, new AllowRefuseAttackEventArgs(targetId, isAttackOk));
    //     }
    //
    //     private void OnMapMessage(uint itemId, uint centerX, uint centerY)
    //     {
    //         _mapMessage?.Invoke(this, new MapMessageEventArgs(itemId, centerX, centerY));
    //     }
    //
    //     private void OnMenu(uint dialogId, ushort menuId)
    //     {
    //         _menu?.Invoke(this, new MenuEventArgs(dialogId, menuId));
    //     }
    //
    //     private void OnDrawObject(uint objectId)
    //     {
    //         _drawObject?.Invoke(this, new ObjectEventArgs(objectId));
    //     }
    //
    //     private void OnUpdateChar(uint objectId)
    //     {
    //         _updateChar?.Invoke(this, new ObjectEventArgs(objectId));
    //     }
    //
    //     private void OnRejectMoveItem(RejectMoveItemReason reason)
    //     {
    //         _rejectMoveItem?.Invoke(this, new RejectMoveItemEventArgs(reason));
    //     }
    //
    //     private void OnAddMultipleItemsInContainer(uint containerId)
    //     {
    //         _addMultipleItemsInContainer?.Invoke(this, new AddMultipleItemsInContainerEventArgs(containerId));
    //     }
    //
    //     private void OnAddItemToContainer(uint itemId, uint containerId)
    //     {
    //         _addItemToContainer?.Invoke(this, new AddItemToContainerEventArgs(itemId, containerId));
    //     }
    //
    //     private void OnDrawContainer(uint containerId, ushort modelGump)
    //     {
    //         _drawContainer?.Invoke(this, new DrawContainerEventArgs(containerId, modelGump));
    //     }
    //
    //     private void OnMoveRejection(ushort xSource, ushort ySource, byte direction, ushort xDest, ushort yDest)
    //     {
    //         _moveRejection?.Invoke(this, new MoveRejectionEventArgs(xSource, ySource, direction, xDest, yDest));
    //     }
    //
    //     private void OnDrawGamePlayer(uint objectId)
    //     {
    //         _drawGamePlayer?.Invoke(this, new ObjectEventArgs(objectId));
    //     }
    //
    //     private void OnSpeech(string text, string senderName, uint senderId)
    //     {
    //         _speech?.Invoke(this, new SpeechEventArgs(text, senderName, senderId));
    //     }
    //
    //     private void OnItemDeleted(uint itemId)
    //     {
    //         _itemDeleted?.Invoke(this, new ItemEventArgs(itemId));
    //     }
    //
    //     private void OnItemInfo(uint itemId)
    //     {
    //         _itemInfo?.Invoke(this, new ItemEventArgs(itemId));
    //     }
    // }
}