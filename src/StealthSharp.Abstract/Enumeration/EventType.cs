#region Copyright

// -----------------------------------------------------------------------
// <copyright file="EventTypes.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Event;
using StealthSharp.Model;
using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Enumeration
{
    /// <summary>
    ///     Event Enumeration.
    /// </summary>
    public enum EventType : byte
    {
        [EventDataType(typeof(Identity))] ItemInfo = 0,
        [EventDataType(typeof(Identity))] ItemDeleted = 1,
        [EventDataType(typeof(SpeechEvent))] Speech = 2,

        [EventDataType(typeof(MoveRejectionEvent))]
        MoveRejection = 4,

        [EventDataType(typeof(DrawContainerEvent))]
        DrawContainer = 5,

        [EventDataType(typeof(AddItemToContainerEvent))]
        AddItemToContainer = 6,
        [EventDataType(typeof(Identity))] AddMultipleItemsInCont = 7,

        [EventDataType(typeof(RejectMoveItemReason))]
        RejectMoveItem = 8,
        [EventDataType(typeof(Identity))] DrawObject = 10,
        [EventDataType(typeof(MenuEvent))] Menu = 11,

        [EventDataType(typeof(MapMessageEvent))]
        MapMessage = 12,

        [EventDataType(typeof(AllowRefuseAttackEvent))]
        AllowRefuseAttack = 13,

        [EventDataType(typeof(ClilocSpeechEvent))]
        ClilocSpeech = 14,

        [EventDataType(typeof(BuffDebuffSystemEvent))]
        BuffDebuffSystem = 17,
        [EventDataType(typeof(EmptyEvent))] ClientSendResync = 18,

        [EventDataType(typeof(CharAnimationEvent))]
        CharAnimation = 19,

        [EventDataType(typeof(IncomingGumpEvent))]
        IncomingGump = 24,

        // 25-26 Timer
        [EventDataType(typeof(uint))] WindowsMessage = 27,
        [EventDataType(typeof(SoundEvent))] Sound = 28,
        [EventDataType(typeof(bool))] Death = 29,

        [EventDataType(typeof(QuestArrowEvent))]
        QuestArrow = 30,
        [EventDataType(typeof(Identity))] PartyInvite = 31,
        [EventDataType(typeof(MapPinEvent))] MapPin = 32,

        [EventDataType(typeof(GumpTextEntryEvent))]
        GumpTextEntry = 33,

        [EventDataType(typeof(GraphicalEffectEvent))]
        GraphicalEffect = 34,

        // 35 - IRC
        [EventDataType(typeof(MessengerIncomingTextEvent))]
        MessengerEvent = 36,

        [EventDataType(typeof(SetGlobalVarEvent))]
        SetGlobalVar = 37,

        [EventDataType(typeof(UpdateObjectStatsEvent))]
        UpdateObjStats = 38,

        [EventDataType(typeof(GlobalChatEvent))]
        GlobalChat = 39
    }
}