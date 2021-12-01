#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Spells.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Enumeration
{
    public enum Spell : uint
    {
        None = 0,

        // Magery Spells
        Clumsy = 1,
        CreateFood = 2,
        Feeblemind = 3,
        Heal = 4,
        MagicArrow = 5,
        NightSight = 6,
        ReactiveArmor = 7,
        Weaken = 8,
        Agility = 9,
        Cunning = 10,
        Cure = 11,
        Harm = 12,
        MagicTrap = 13,
        MagicUntrap = 14,
        Protection = 15,
        Strength = 16,
        Bless = 17,
        Fireball = 18,
        MagicLock = 19,
        Poison = 20,
        Telekinesis = 21,
        Teleport = 22,
        Unlock = 23,
        WallOfStone = 24,
        ArchCure = 25,
        ArchProtection = 26,
        Curse = 27,
        FireField = 28,
        GreaterHeal = 29,
        Lightning = 30,
        ManaDrain = 31,
        Recall = 32,
        BladeSpirit = 33,
        DispelField = 34,
        Incognito = 35,
        MagicReflection = 36,
        SpellReflection = MagicReflection,
        MindBlast = 37,
        Paralyze = 38,
        PoisonField = 39,
        SummonCreature = 40,
        Dispel = 41,
        EnergyBolt = 42,
        Explosion = 43,
        Invisibility = 44,
        Mark = 45,
        MassCurse = 46,
        ParalyzeField = 47,
        Reveal = 48,
        ChainLightning = 49,
        EnergyField = 50,
        FlameStrike = 51,
        GateTravel = 52,
        ManaVampire = 53,
        MassDispel = 54,
        MeteorSwarm = 55,
        Polymorph = 56,
        Earthquake = 57,
        EnergyVortex = 58,
        Resurrection = 59,
        SummonAirElemental = 60,
        SummonDaemon = 61,
        SummonEarthElemental = 62,
        SummonFireElemental = 63,
        SummonWaterElemental = 64,

        // Necromancy
        AnimateDead = 101,
        BloodOath = 102,
        CorpseSkin = 103,
        CurseWeapon = 104,
        EvilOmen = 105,
        HorrificBeast = 106,
        LichForm = 107,
        MindRot = 108,
        PainSpike = 109,
        PoisonStrike = 110,
        Strangle = 111,
        SummonFamiliar = 112,
        VampiricEmbrace = 113,
        VengefulSpirit = 114,
        Wither = 115,
        WraithForm = 116,
        Exorcism = 117,

        // Chivalry
        CleanseByFire = 201,
        CloseWounds = 202,
        ConsecrateWeapon = 203,
        DispelEvil = 204,
        DivineFury = 205,
        EnemyOfOne = 206,
        HolyLight = 207,
        NobleSacrifice = 208,
        RemoveCurse = 209,
        SacredJourney = 210,

        // Bushido
        HonorableExecution = 401,
        Confidence = 402,
        Evasion = 403,
        CounterAttack = 404,
        LightningStrike = 405,
        MomentumStrike = 406,

        // Ninjitsu
        FocusAttack = 501,
        DeathStrike = 502,
        AnimalForm = 503,
        KiAttack = 504,
        SupriseAttack = 505,
        Backstab = 506,
        Shadowjump = 507,
        MirrorImage = 508,

        // Spellweaving
        ArcaneCircle = 601,
        GiftOfRenewal = 602,
        ImmolatingWeapon = 603,
        Attunement = 604,
        Thunderstorm = 605,
        NatureFury = 606,
        SummonFey = 607,
        SummonFiend = 608,
        ReaperForm = 609,
        Wildfire = 610,
        EssenceOfWind = 611,
        DryadAllure = 612,
        EtherealVoyage = 613,
        WordOfDeath = 614,
        GiftOfLife = 615,
        ArcaneEmpowerment = 616,

        // Mysticism spells
        NetherBolt = 678,
        HealingStone = 679,
        PureMagic = 680,
        Enchant = 681,
        Sleep = 682,
        EagleStrike = 683,
        AnimatedWeapon = 684,
        StoneForm = 685,
        SpellTrigger = 686,
        MassSleep = 687,
        CleansingWinds = 688,
        Bombard = 689,
        SpellPlague = 690,
        HailStorm = 691,
        NetherCyclone = 692,
        RisingColossus = 693,

        // Provocation
        Inspire = 701,
        Invigorate = 702,

        // Peacemaking
        Resilience = 703,
        Perseverance = 704,

        // Discordance
        Tribulation = 705,
        Despair = 706,

        // Magery
        DeathRay = 707,
        EtherealBurst = 708,

        // Mysticism
        NetherBlast = 709,
        MysticWeapon = 710,

        // Necromancy
        CommandUndead = 711,
        Conduit = 712,

        // Spellweaving
        ManaShield = 713,
        SummonReaper = 714,

        // Bushido
        AnticipateHit = 716,
        Warcry = 717,

        // Chivalry
        Rejuvenate = 719,
        HolyFist = 720,

        // Ninjitsu
        Shadow = 721,
        WhiteTigerForm = 722,

        // Archery
        FlamingShot = 723,
        PlayingTheOdds = 724,

        // Fencing
        Thrust = 725,
        Pierce = 726,

        // Mace Fighting
        Stagger = 727,
        Toughness = 728,

        // Swordsmanship
        Onslaught = 729,
        FocusedEye = 730,

        // Throwing
        ElementalFury = 731,
        CalledShot = 732,

        // Parrying
        ShieldBash = 734,
        Bodyguard = 735,
        HeightenSenses = 736,

        // Poisoning
        Tolerance = 737,
        InjectedStrike = 738,
        Potency = 739,

        // Wrestling
        Rampage = 740,
        FistsOfFury = 741,
        Knockout = 742,

        // Animal Taming
        Whispering = 743,
        CombatTraining = 744,
        Boarding = 745,

        // Shared Passives
        EnchantedSummoning = 715,
        Intuition = 718,
        WarriorsGifts = 733,
    }
}