#region Copyright

// -----------------------------------------------------------------------
// <copyright file="RejectMoveItemReasons.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Enumeration
{
    public enum RejectMoveItemReason : byte
    {
        CanNotPickUp = 0,
        TooFarAway = 1,
        OutOfSight = 2,
        DoesNotBelong = 3,
        AlreadyHolding = 4,
        MustWait = 5
    }
}