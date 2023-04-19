#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MessangerEventType.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Enumeration
{
    public enum MessengerEventType : byte
    {
        Connected = 0,
        Disconnected = 1,
        Message = 2,
        Error = 3
    }
}