#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IClientService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;

namespace StealthSharp.Services
{
    public interface IClientService
    {
        Task<bool> ClientHideAsync(uint iD);

        void ClientPrint(string text);

        void ClientPrintEx(uint senderID, ushort color, ushort font, string text);

        void CloseClientUIWindow(UIWindowType uIWindowType, uint iD);

        void UOSay(string text);

        void UOSayColor(string text, ushort color);
    }
}