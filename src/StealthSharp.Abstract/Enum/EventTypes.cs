#region Copyright

// -----------------------------------------------------------------------
// <copyright file="EventTypes.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Enum
{
    /// <summary>
    ///     Event Enumeration.
    /// </summary>
    public enum EventTypes : byte
    {
        ItemInfo = 0,
        ItemDeleted = 1,
        Speech = 2,
        DrawGamePlayer = 3,
        MoveRejection = 4,
        DrawContainer = 5,
        AddItemToContainer = 6,
        AddMultipleItemsInCont = 7,
        RejectMoveItem = 8,
        UpdateChar = 9,
        DrawObject = 10,
        Menu = 11,
        MapMessage = 12,
        Allow_RefuseAttack = 13,
        ClilocSpeech = 14,
        ClilocSpeechAffix = 15,
        UnicodeSpeech = 16,
        Buff_DebuffSystem = 17,
        ClientSendResync = 18,
        CharAnimation = 19,
        ICQDisconnect = 20,
        ICQConnect = 21,
        ICQIncomingText = 22,
        ICQError = 23,
        IncomingGump = 24,
        Timer1 = 25,
        Timer2 = 26,
        WindowsMessage = 27,
        Sound = 28,
        Death = 29,
        QuestArrow = 30,
        PartyInvite = 31,
        MapPin = 32,
        GumpTextEntry = 33,
        GraphicalEffect = 34,
        IRCIncomingText = 35,
        MessengerEvent = 36,
        SetGlobalVar = 37,
        UpdateObjStats = 38,
    }
}