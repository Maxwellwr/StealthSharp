#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ExtendedInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    /// <summary>
    ///     Extended Info.
    /// </summary>
    public class ExtendedInfo
    {
        [PacketData(0, 2)] public ushort MaxWeight { get; set; }
        [PacketData(2, 1)] public byte Race { get; set; }
        [PacketData(3, 2)] public ushort StatCap { get; set; }
        [PacketData(5, 1)] public byte PetsCurrent { get; set; }
        [PacketData(6, 1)] public byte PetsMax { get; set; }
        [PacketData(7, 2)] public ushort FireResist { get; set; }
        [PacketData(9, 2)] public ushort ColdResist { get; set; }
        [PacketData(11, 2)] public ushort PoisonResist { get; set; }
        [PacketData(13, 2)] public ushort EnergyResist { get; set; }
        [PacketData(15, 2)] public short Luck { get; set; }
        [PacketData(17, 2)] public ushort DamageMin { get; set; }
        [PacketData(19, 2)] public ushort DamageMax { get; set; }
        [PacketData(21, 4)] public uint TithingPoints { get; set; }
        [PacketData(25, 2)] public ushort HitChanceIncr { get; set; }
        [PacketData(27, 2)] public ushort SwingSpeedIncr { get; set; }
        [PacketData(29, 2)] public ushort DamageChanceIncr { get; set; }
        [PacketData(31, 2)] public ushort LowerReagentCost { get; set; }
        [PacketData(33, 2)] public ushort HpRegen { get; set; }
        [PacketData(35, 2)] public ushort StamRegen { get; set; }
        [PacketData(37, 2)] public ushort ManaRegen { get; set; }
        [PacketData(39, 2)] public ushort ReflectPhysDamage { get; set; }
        [PacketData(41, 2)] public ushort EnhancePotions { get; set; }
        [PacketData(43, 2)] public ushort DefenseChanceIncr { get; set; }
        [PacketData(45, 2)] public ushort SpellDamageIncr { get; set; }
        [PacketData(47, 2)] public ushort FasterCastRecovery { get; set; }
        [PacketData(49, 2)] public ushort FasterCasting { get; set; }
        [PacketData(51, 2)] public ushort LowerManaCost { get; set; }
        [PacketData(53, 2)] public ushort StrengthIncr { get; set; }
        [PacketData(55, 2)] public ushort DextIncr { get; set; }
        [PacketData(57, 2)] public ushort IntIncr { get; set; }
        [PacketData(59, 2)] public ushort HpIncr { get; set; }
        [PacketData(61, 2)] public ushort StamIncr { get; set; }
        [PacketData(63, 2)] public ushort ManaIncr { get; set; }
        [PacketData(65, 2)] public ushort MaxHpIncr { get; set; }
        [PacketData(67, 2)] public ushort MaxStamIncr { get; set; }
        [PacketData(69, 2)] public ushort MaxManaIncrease { get; set; }
    }
}