#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMenuService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Model;

#endregion

namespace StealthSharp.Services
{
    public interface IMenuService
    {
        Task<List<string>> GetLastMenuItemsAsync();

        /// <summary>
        ///     If the menu traps were set(<see cref="WaitMenuAsync" />, <see cref="AutoMenuAsync" />) returns True, otherwise False.
        /// </summary>
        Task<bool> GetMenuHookPresentAsync();

        /// <summary>
        ///     Returns True, if there is an active menu. False - no.
        /// </summary>
        Task<bool> GetMenuPresentAsync();

        /// <summary>
        ///     Reusable set a trap on the menu. Works the same as <see cref="WaitMenuAsync" />, the only difference,
        ///     WaitMenu work out only once, and the trap is removed, AutoMenu - runs continuously.
        /// </summary>
        /// <param name="menuCaption">Menu caption.</param>
        /// <param name="elementCaption">Element caption.</param>
        Task AutoMenuAsync(string menuCaption, string elementCaption);

        /// <summary>
        ///     Remove all traps set on the menu.
        /// </summary>
        Task CancelMenuAsync();

        Task CloseMenuAsync();

        Task<List<string>> GetMenuItemsAsync(string menuCaption);

        Task<List<MenuItem>> GetMenuItemsExAsync(string menuCaption);

        /// <summary>
        ///     Установить одноразовую ловушку на меню. Является частным случаем многоразовой ловушки <see cref="AutoMenuAsync" />.
        ///     Впрочем, абсолютно так же может использоваться и для обработки уже пришедших меню.
        ///     Работает так: начинает перебирать меню от первого пришедшего до последнего пришедшего.
        ///     В каждом из перебираемых меню сверяет заголовок меню на предмет совпадения заголовка с параметром функции
        ///     MenuCaption.
        ///     Если есть совпадение - то в этом меню ищется элемент с названием ElementCaption.
        ///     Если таковой имеется - то перебор прекращается, и отсылается ответ на меню серверу с этим элементом, а в стелсе
        ///     меню уничтожается.
        ///     Если такой элемент (или меню) не найден - то ловушка устанавливается для сверки с вновь приходящими меню.
        /// </summary>
        /// <param name="menuCaption">Menu caption.</param>
        /// <param name="elementCaption">Element caption.</param>
        Task WaitMenuAsync(string menuCaption, string elementCaption);

        Task<bool> WaitForMenuPresentAsync(int timeout);
    }
}