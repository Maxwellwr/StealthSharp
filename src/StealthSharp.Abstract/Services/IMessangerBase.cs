#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IMessangerBase.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;

namespace StealthSharp.Services
{
    public interface IMessangerBase
    {
        // event EventHandler Connected;
        //
        // event EventHandler Disconnected;

        // event EventHandler<MessangerTextEventArgs> IncomingMessage;
        //
        // event EventHandler<MessangerErrorEventArgs> Error;

        Task<bool> GetIsConnectedAsync();

        Task<string> GetTokenAsync();

        Task<string> GetNameAsync();

        void SendMessage(string message, string recieverId);

        void Connect(string token);
    }
}