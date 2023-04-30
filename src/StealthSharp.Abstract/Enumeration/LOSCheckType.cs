#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="LOSCheckType.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

namespace StealthSharp.Enumeration;

public enum LOSCheckType: byte
{
    Sphere = 1,
    SphereAdv = 2,
    POL = 3,
    RunUo = 4,
    ServUo = RunUo
}