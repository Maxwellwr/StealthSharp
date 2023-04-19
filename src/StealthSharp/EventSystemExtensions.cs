#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="EventSystemExtensions.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.Model;
using StealthSharp.Services;

#endregion

namespace StealthSharp
{
    public static class EventSystemExtensions
    {
        public static Task<IEventSystemService> OnItemInfo(this Task<IEventSystemService> service,
            Action<Identity> action)
        {
            return service.ContinueWith(s => s.Result.OnItemInfo(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnItemInfo(this IEventSystemService service,
            Action<Identity> action)
        {
            await service.Subscribe(EventType.ItemInfo, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnItemDeleted(this Task<IEventSystemService> service,
            Action<Identity> action)
        {
            return service.ContinueWith(s => s.Result.OnItemDeleted(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnItemDeleted(this IEventSystemService service,
            Action<Identity> action)
        {
            await service.Subscribe(EventType.ItemDeleted, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnSpeech(this Task<IEventSystemService> service,
            Action<SpeechEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnSpeech(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnSpeech(this IEventSystemService service,
            Action<SpeechEvent> action)
        {
            await service.Subscribe(EventType.Speech, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnMoveRejection(this Task<IEventSystemService> service,
            Action<MoveRejectionEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnMoveRejection(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnMoveRejection(this IEventSystemService service,
            Action<MoveRejectionEvent> action)
        {
            await service.Subscribe(EventType.MoveRejection, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnDrawContainer(this Task<IEventSystemService> service,
            Action<DrawContainerEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnDrawContainer(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnDrawContainer(this IEventSystemService service,
            Action<DrawContainerEvent> action)
        {
            await service.Subscribe(EventType.DrawContainer, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnAddItemToContainer(this Task<IEventSystemService> service,
            Action<AddItemToContainerEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnAddItemToContainer(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnAddItemToContainer(this IEventSystemService service,
            Action<AddItemToContainerEvent> action)
        {
            await service.Subscribe(EventType.AddItemToContainer, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnAddMultipleItemsInCont(this Task<IEventSystemService> service,
            Action<Identity> action)
        {
            return service.ContinueWith(s => s.Result.OnAddMultipleItemsInCont(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnAddMultipleItemsInCont(this IEventSystemService service,
            Action<Identity> action)
        {
            await service.Subscribe(EventType.AddMultipleItemsInCont, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnRejectMoveItem(this Task<IEventSystemService> service,
            Action<RejectMoveItemReason> action)
        {
            return service.ContinueWith(s => s.Result.OnRejectMoveItem(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnRejectMoveItem(this IEventSystemService service,
            Action<RejectMoveItemReason> action)
        {
            await service.Subscribe(EventType.RejectMoveItem, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnDrawObject(this Task<IEventSystemService> service,
            Action<Identity> action)
        {
            return service.ContinueWith(s => s.Result.OnDrawObject(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnDrawObject(this IEventSystemService service,
            Action<Identity> action)
        {
            await service.Subscribe(EventType.DrawObject, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnMenu(this Task<IEventSystemService> service,
            Action<MenuEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnMenu(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnMenu(this IEventSystemService service, Action<MenuEvent> action)
        {
            await service.Subscribe(EventType.Menu, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnMapMessage(this Task<IEventSystemService> service,
            Action<MapMessageEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnMapMessage(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnMapMessage(this IEventSystemService service,
            Action<MapMessageEvent> action)
        {
            await service.Subscribe(EventType.MapMessage, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnAllowRefuseAttack(this Task<IEventSystemService> service,
            Action<AllowRefuseAttackEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnAllowRefuseAttack(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnAllowRefuseAttack(this IEventSystemService service,
            Action<AllowRefuseAttackEvent> action)
        {
            await service.Subscribe(EventType.AllowRefuseAttack, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnClilocSpeech(this Task<IEventSystemService> service,
            Action<ClilocSpeechEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnClilocSpeech(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnClilocSpeech(this IEventSystemService service,
            Action<ClilocSpeechEvent> action)
        {
            await service.Subscribe(EventType.ClilocSpeech, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnBuffDebuffSystem(this Task<IEventSystemService> service,
            Action<BuffDebuffSystemEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnBuffDebuffSystem(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnBuffDebuffSystem(this IEventSystemService service,
            Action<BuffDebuffSystemEvent> action)
        {
            await service.Subscribe(EventType.BuffDebuffSystem, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnClientSendResync(this Task<IEventSystemService> service,
            Action<EmptyEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnClientSendResync(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnClientSendResync(this IEventSystemService service,
            Action<EmptyEvent> action)
        {
            await service.Subscribe(EventType.ClientSendResync, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnCharAnimation(this Task<IEventSystemService> service,
            Action<CharAnimationEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnCharAnimation(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnCharAnimation(this IEventSystemService service,
            Action<CharAnimationEvent> action)
        {
            await service.Subscribe(EventType.CharAnimation, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnIncomingGump(this Task<IEventSystemService> service,
            Action<IncomingGumpEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnIncomingGump(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnIncomingGump(this IEventSystemService service,
            Action<IncomingGumpEvent> action)
        {
            await service.Subscribe(EventType.IncomingGump, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnWindowsMessage(this Task<IEventSystemService> service,
            Action<uint> action)
        {
            return service.ContinueWith(s => s.Result.OnWindowsMessage(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnWindowsMessage(this IEventSystemService service,
            Action<uint> action)
        {
            await service.Subscribe(EventType.WindowsMessage, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnSound(this Task<IEventSystemService> service,
            Action<SoundEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnSound(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnSound(this IEventSystemService service,
            Action<SoundEvent> action)
        {
            await service.Subscribe(EventType.Sound, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnDeath(this Task<IEventSystemService> service,
            Action<bool> action)
        {
            return service.ContinueWith(s => s.Result.OnDeath(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnDeath(this IEventSystemService service, Action<bool> action)
        {
            await service.Subscribe(EventType.Death, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnQuestArrow(this Task<IEventSystemService> service,
            Action<QuestArrowEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnQuestArrow(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnQuestArrow(this IEventSystemService service,
            Action<QuestArrowEvent> action)
        {
            await service.Subscribe(EventType.QuestArrow, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnPartyInvite(this Task<IEventSystemService> service,
            Action<Identity> action)
        {
            return service.ContinueWith(s => s.Result.OnPartyInvite(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnPartyInvite(this IEventSystemService service,
            Action<Identity> action)
        {
            await service.Subscribe(EventType.PartyInvite, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnMapPin(this Task<IEventSystemService> service,
            Action<MapPinEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnMapPin(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnMapPin(this IEventSystemService service,
            Action<MapPinEvent> action)
        {
            await service.Subscribe(EventType.MapPin, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnGumpTextEntry(this Task<IEventSystemService> service,
            Action<GumpTextEntryEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnGumpTextEntry(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnGumpTextEntry(this IEventSystemService service,
            Action<GumpTextEntryEvent> action)
        {
            await service.Subscribe(EventType.GumpTextEntry, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnGraphicalEffect(this Task<IEventSystemService> service,
            Action<GraphicalEffectEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnGraphicalEffect(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnGraphicalEffect(this IEventSystemService service,
            Action<GraphicalEffectEvent> action)
        {
            await service.Subscribe(EventType.GraphicalEffect, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnMessengerEvent(this Task<IEventSystemService> service,
            Action<MessengerIncomingTextEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnMessengerEvent(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnMessengerEvent(this IEventSystemService service,
            Action<MessengerIncomingTextEvent> action)
        {
            await service.Subscribe(EventType.MessengerEvent, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnSetGlobalVar(this Task<IEventSystemService> service,
            Action<SetGlobalVarEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnSetGlobalVar(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnSetGlobalVar(this IEventSystemService service,
            Action<SetGlobalVarEvent> action)
        {
            await service.Subscribe(EventType.SetGlobalVar, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnUpdateObjectStats(this Task<IEventSystemService> service,
            Action<UpdateObjectStatsEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnUpdateObjectStats(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnUpdateObjectStats(this IEventSystemService service,
            Action<UpdateObjectStatsEvent> action)
        {
            await service.Subscribe(EventType.UpdateObjStats, action).ConfigureAwait(false);
            return service;
        }

        public static Task<IEventSystemService> OnGlobalChat(this Task<IEventSystemService> service,
            Action<GlobalChatEvent> action)
        {
            return service.ContinueWith(s => s.Result.OnGlobalChat(action)).Unwrap();
        }

        public static async Task<IEventSystemService> OnGlobalChat(this IEventSystemService service,
            Action<GlobalChatEvent> action)
        {
            await service.Subscribe(EventType.GlobalChat, action).ConfigureAwait(false);
            return service;
        }
    }
}