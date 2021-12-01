#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ExtendedInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     Extended Info.
    /// </summary>
    [Serialization.Serializable()]
    public class ExtendedInfo
    {
        public ushort MaxWeight { get; set; }
        public byte Race { get; set; }
        public ushort StatCap { get; set; }
        public byte PetsCurrent { get; set; }
        public byte PetsMax { get; set; }
        public ushort FireResist { get; set; }
        public ushort ColdResist { get; set; }
        public ushort PoisonResist { get; set; }
        public ushort EnergyResist { get; set; }
        public short Luck { get; set; }
        public ushort DamageMin { get; set; }
        public ushort DamageMax { get; set; }
        public uint TithingPoints { get; set; }
        public ushort HitChanceIncr { get; set; }
        public ushort SwingSpeedIncr { get; set; }
        public ushort DamageChanceIncr { get; set; }
        public ushort LowerReagentCost { get; set; }
        public ushort HpRegen { get; set; }
        public ushort StamRegen { get; set; }
        public ushort ManaRegen { get; set; }
        public ushort ReflectPhysDamage { get; set; }
        public ushort EnhancePotions { get; set; }
        public ushort DefenseChanceIncr { get; set; }
        public ushort SpellDamageIncr { get; set; }
        public ushort FasterCastRecovery { get; set; }
        public ushort FasterCasting { get; set; }
        public ushort LowerManaCost { get; set; }
        public ushort StrengthIncr { get; set; }
        public ushort DextIncr { get; set; }
        public ushort IntIncr { get; set; }
        public ushort HpIncr { get; set; }
        public ushort StamIncr { get; set; }
        public ushort ManaIncr { get; set; }
        public ushort MaxHpIncr { get; set; }
        public ushort MaxStamIncr { get; set; }
        public ushort MaxManaIncrease { get; set; }
    }
}