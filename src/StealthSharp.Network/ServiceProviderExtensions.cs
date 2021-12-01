#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ServiceProviderExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using StealthSharp.Network;
using StealthSharp.Serialization;

// ReSharper disable CheckNamespace Microsoft DI Extension methods recommend to place in Microsoft namespace https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage#register-services-for-di  
namespace Microsoft.Extensions.DependencyInjection
// ReSharper restore CheckNamespace
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddStealthSharpClient(
            this IServiceCollection serviceCollection)
            => AddStealthSharpClient(serviceCollection, _ => { });

        public static IServiceCollection AddStealthSharpClient(
            this IServiceCollection serviceCollection, Action<SerializationOptions> configAction)
        {
            serviceCollection.AddLogging();
            serviceCollection.AddStealthSharpSerialization(configAction);
            serviceCollection
                .AddSingleton<IStealthSharpClient, StealthSharpClient>();
            return serviceCollection;
        }
    }
}