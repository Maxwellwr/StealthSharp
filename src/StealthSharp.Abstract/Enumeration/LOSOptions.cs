#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="LOSOptions.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Enumeration;

[Flags]
public enum LOSOptions : uint
{
    None = 0,
    SphereCheckCorners = 256,
    PolUseNoShoot = 512,
    PolLOSThroughWindow = 1024
}