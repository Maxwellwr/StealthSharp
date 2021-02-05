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

        void ICQConnect(string uIN, string password);

        void ICQDisconnect();

        void ICQSendText(string destinationUIN, string text);

        void ICQSetStatus(byte num);

        void ICQSetXStatus(byte num);
    }
}