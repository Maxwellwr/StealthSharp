#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="StealthSharp.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Network;
using StealthSharp.Services;

namespace StealthSharp
{
    public class Stealth
    {
        private readonly IServiceProvider _serviceProvider;
        
        public IConnectionService Connection => _serviceProvider.GetRequiredService<IConnectionService>();

        public IClientService Client => _serviceProvider.GetRequiredService<IClientService>();

        public IJournalService Journal => _serviceProvider.GetRequiredService<IJournalService>();

        public IObjectSearchService Search => _serviceProvider.GetRequiredService<IObjectSearchService>();

        public IGameObjectService GameObject => _serviceProvider.GetRequiredService<IGameObjectService>();

        public IMoveItemService MoveItem => _serviceProvider.GetRequiredService<IMoveItemService>();

        public IMoveService Move => _serviceProvider.GetRequiredService<IMoveService>();

        public ITargetService Target => _serviceProvider.GetRequiredService<ITargetService>();

        public ICharStatsService Char => _serviceProvider.GetRequiredService<ICharStatsService>();

        public IGumpService Gump => _serviceProvider.GetRequiredService<IGumpService>();

        public ISkillSpellService SkillSpell => _serviceProvider.GetRequiredService<ISkillSpellService>();

        public IViberService Viber => _serviceProvider.GetRequiredService<IViberService>();

        public ITelegramService Telegram => _serviceProvider.GetRequiredService<ITelegramService>();

        public T GetStealthService<T>() where T : notnull => _serviceProvider.GetRequiredService<T>();
        
        public Stealth(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ConnectToStealth()
        {
            var internalService = _serviceProvider.GetRequiredService<InternalService>();
            internalService.ConnectToStealth();
        }
    }
}