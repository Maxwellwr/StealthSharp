﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="TileDataFlags.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Enumeration
{
    [Flags]
	public enum TileDataFlags : ulong
	{
		None = 0x0000000000000000,

		Background = 0x0000000000000001,
		Weapon = 0x0000000000000002,
		Transparent = 0x0000000000000004,
		Translucent = 0x0000000000000008,
		Wall = 0x0000000000000010,
		Damaging = 0x0000000000000020,
		Impassable = 0x0000000000000040,
		Wet = 0x0000000000000080,
		Unknown1 = 0x0000000000000100,
		Surface = 0x0000000000000200,
		Bridge = 0x0000000000000400,
		Generic = 0x0000000000000800,
		Window = 0x0000000000001000,
		NoShoot = 0x0000000000002000,
		ArticleA = 0x0000000000004000,
		ArticleAn = 0x0000000000008000,
		Internal = 0x0000000000010000,
		Foliage = 0x0000000000020000,
		PartialHue = 0x0000000000040000,
		Unknown2 = 0x0000000000080000,
		Map = 0x0000000000100000,
		Container = 0x0000000000200000,
		Wearable = 0x0000000000400000,
		LightSource = 0x0000000000800000,
		Animation = 0x0000000001000000,
		HoverOver = 0x0000000002000000,
		Unknown3 = 0x0000000004000000,
		Armor = 0x0000000008000000,
		Roof = 0x0000000010000000,
		Door = 0x0000000020000000,
		StairBack = 0x0000000040000000,
		StairRight = 0x0000000080000000,

		HS33 = 0x0000000100000000,
		HS34 = 0x0000000200000000,
		HS35 = 0x0000000400000000,
		HS36 = 0x0000000800000000,
		HS37 = 0x0000001000000000,
		HS38 = 0x0000002000000000,
		HS39 = 0x0000004000000000,
		HS40 = 0x0000008000000000,
		HS41 = 0x0000010000000000,
		HS42 = 0x0000020000000000,
		HS43 = 0x0000040000000000,
		HS44 = 0x0000080000000000,
		HS45 = 0x0000100000000000,
		HS46 = 0x0000200000000000,
		HS47 = 0x0000400000000000,
		HS48 = 0x0000800000000000,
		HS49 = 0x0001000000000000,
		HS50 = 0x0002000000000000,
		HS51 = 0x0004000000000000,
		HS52 = 0x0008000000000000,
		HS53 = 0x0010000000000000,
		HS54 = 0x0020000000000000,
		HS55 = 0x0040000000000000,
		HS56 = 0x0080000000000000,
		HS57 = 0x0100000000000000,
		HS58 = 0x0200000000000000,
		HS59 = 0x0400000000000000,
		HS60 = 0x0800000000000000,
		HS61 = 0x1000000000000000,
		HS62 = 0x2000000000000000,
		HS63 = 0x4000000000000000,
		HS64 = 0x8000000000000000
	}
}