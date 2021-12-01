#region Copyright

// -----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Network;

namespace StealthSharp.Services
{
    public abstract class BaseService
    {
        protected BaseService(IStealthSharpClient client)
        {
            Client = client;
        }

        protected IStealthSharpClient Client { get; }
    }
}