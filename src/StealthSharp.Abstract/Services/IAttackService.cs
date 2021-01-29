#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IAttackService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IAttackService
    {
        /// <summary>
        ///     Last attacked object.
        /// </summary>
        Task<uint> GetLastAttackAsync();

        /// <summary>
        ///     Set the state of combat mode (War).
        /// </summary>
        Task SetWarModeAsync(bool value);

        /// <summary>
        ///     Returns the state of combat mode (War).
        /// </summary>
        Task<bool> GetWarModeAsync();

        /// <summary>
        ///     Return the ID of the currently attacked target.
        ///     If your character is not in war mode or it is not connected it will return 0.
        ///     <example>
        ///         IAttackService _attack;
        ///         uint _enemy;
        ///         if (_attack.WarTargetID <> _enemy) then
        ///         _attack.Attack(_enemy);
        ///     </example>
        /// </summary>
        Task<uint> GetWarTargetIDAsync();

        /// <summary>
        ///     Throw an attack on the object ObjdID.
        ///     If you are not in War mode, the client can install it before the attack.
        /// </summary>
        /// <example>
        ///     IAttackService _attack;
        ///     uint _enemy;
        ///     if (_attack.WarTargetID. <> _enemy) then
        ///     _attack.Attack(_enemy);
        /// </example>
        /// <param name="attackedId">Object for attack</param>
        Task AttackAsync(uint attackedId);
    }
}