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
using StealthSharp.Services;

namespace StealthSharp
{
    public class Stealth
    {
        private readonly IServiceProvider _serviceProvider;
        
        public IConnectionService Connection => GetStealthService<IConnectionService>();

        public IClientService Client => GetStealthService<IClientService>();

        public IJournalService Journal => GetStealthService<IJournalService>();

        public IObjectSearchService Search => GetStealthService<IObjectSearchService>();

        public IGameObjectService GameObject => GetStealthService<IGameObjectService>();

        public IMoveItemService MoveItem => GetStealthService<IMoveItemService>();

        public IMoveService Move => GetStealthService<IMoveService>();

        public ITargetService Target => GetStealthService<ITargetService>();

        public ICharStatsService Char => GetStealthService<ICharStatsService>();
        
        public IAttackService Attack => GetStealthService<IAttackService>();

        public ILayerService Layer => GetStealthService<ILayerService>();

        public IMenuService Menu => GetStealthService<IMenuService>();

        public IGumpService Gump => GetStealthService<IGumpService>();

        public ISkillSpellService SkillSpell => GetStealthService<ISkillSpellService>();

        public IViberService Viber => GetStealthService<IViberService>();

        public ITelegramService Telegram => GetStealthService<ITelegramService>();

        public T GetStealthService<T>() where T : notnull => _serviceProvider.GetRequiredService<T>();
        
        public Stealth(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ConnectToStealthAsync()
        {
            var internalService = GetStealthService<InternalService>();
            await internalService.ConnectToStealthAsync().ConfigureAwait(false);
        }
    }
}