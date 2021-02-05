#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ServiceProviderExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using Microsoft.Extensions.Options;
using StealthSharp;
using StealthSharp.Network;
using StealthSharp.Serialization;
using StealthSharp.Services;

// ReSharper disable CheckNamespace Microsoft DI Extension methods recommend to place in Microsoft namespace https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage#register-serviceCollection-for-di  
namespace Microsoft.Extensions.DependencyInjection
// ReSharper restore CheckNamespace
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddStealthSharp(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddStealthSharpClient<ushort, uint, ushort, StealthTypeMapper>(c =>
            {
                c.ArrayCountType = typeof(uint);
                c.StringSizeType = typeof(uint);
            });
            serviceCollection.AddSingleton<IPacketCorrelationGenerator<ushort>, PacketCorrelationGenerator>();
            serviceCollection.AddSingleton<ICustomConverter<DateTime>, DateTimeConverter>();
            serviceCollection.AddTransient(typeof(IPacket<,,,>), typeof(Packet<,,,>));
            serviceCollection.AddTransient(typeof(IPacket<,,>), typeof(Packet<,,>));
            serviceCollection.AddServices();
            serviceCollection.AddSingleton<Stealth, Stealth>();
            serviceCollection.AddTransient<InternalService, InternalService>();
            return serviceCollection;
        }

        private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAttackService, AttackService>();
            serviceCollection.AddTransient<ICharStatsService, CharStatsService>();
            serviceCollection.AddTransient<IClientService, ClientService>();
            serviceCollection.AddTransient<IConnectionService, ConnectionService>();
            serviceCollection.AddTransient<IContextMenuService, ContextMenuService>();
            //serviceCollection.AddSingleton<IEventSystemService, EventSystemService>();
            serviceCollection.AddTransient<IGameObjectService, GameObjectService>();
            serviceCollection.AddTransient<IGestureService, GestureService>();
            serviceCollection.AddTransient<IGlobalService, GlobalService>();
            serviceCollection.AddTransient<IGumpService, GumpService>();
            serviceCollection.AddTransient<IICQService, ICQService>();
            serviceCollection.AddTransient<IInfoWindowService, InfoWindowService>();
            serviceCollection.AddTransient<IJournalService, JournalService>();
            serviceCollection.AddTransient<ILayerService, LayerService>();
            serviceCollection.AddTransient<IMapService, MapService>();
            serviceCollection.AddTransient<IMarketService, MarketService>();
            serviceCollection.AddTransient<IMenuService, MenuService>();
            serviceCollection.AddTransient<IMoveItemService, MoveItemService>();
            serviceCollection.AddTransient<IMoveService, MoveService>();
            serviceCollection.AddTransient<IObjectSearchService, ObjectSearchService>();
            serviceCollection.AddTransient<IPartyService, PartyService>();
            serviceCollection.AddTransient<IReagentService, ReagentService>();
            serviceCollection.AddTransient<ISkillSpellService, SkillSpellService>();
            serviceCollection.AddTransient<IStealthService, StealthService>();
            serviceCollection.AddTransient<ISystemService, SystemService>();
            serviceCollection.AddTransient<ITargetService, TargetService>();
            serviceCollection.AddTransient<ITileService, TileService>();
            serviceCollection.AddTransient<ITradeService, TradeService>();
            serviceCollection.AddTransient<IGlobalChatService, GlobalChatService>();
            //serviceCollection.AddTransient<ITelegramService, TelegramService>();
            //serviceCollection.AddTransient<IViberService, ViberService>();
            return serviceCollection;
        }
    }
}