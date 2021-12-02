#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IClientService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enumeration;

namespace StealthSharp.Services
{
    public interface IClientService
    {
        Task<bool> ClientHideAsync(uint id);

        Task ClientPrintAsync(string text);

        Task ClientPrintExAsync(uint senderId, ushort color, ushort font, string text);

        Task CloseClientUIWindowAsync(UIWindowType uIWindowType, uint id);

        Task UOSayAsync(string text);

        Task UOSayColorAsync(string text, ushort color);
    }
}