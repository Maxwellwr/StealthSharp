#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IICQService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IICQService
    {
        Task<bool> GetICQConnectedAsync();

        Task ICQConnectAsync(string uIN, string password);

        Task ICQDisconnectAsync();

        Task ICQSendTextAsync(string destinationUIN, string text);

        Task ICQSetStatusAsync(byte num);

        Task ICQSetXStatusAsync(byte num);
    }
}