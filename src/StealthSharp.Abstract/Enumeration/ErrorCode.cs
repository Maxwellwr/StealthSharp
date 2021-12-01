#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="ErrorCode.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

namespace StealthSharp.Enumeration
{
    public enum ErrorCode:byte
    {
        PacketSizeTooSmall = 0,
        UnfinishedPacket = 1,
        ZeroMethodNum = 2,
        UnfinishedReceive = 3,
        PacketProcessError =4,
        UnknownScriptLanguage = 5,
        OldVersion = 6,
        MethodNotFound = 7,
        MethodNotFoundInUO = 8
    }
}