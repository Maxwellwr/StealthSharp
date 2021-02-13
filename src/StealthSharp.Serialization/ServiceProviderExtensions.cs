#region Copyright

// -----------------------------------------------------------------------
// <copyright file="ServiceProviderExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using StealthSharp.Serialization;

// ReSharper disable CheckNamespace Microsoft DI Extension methods recommend to place in Microsoft namespace https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage#register-services-for-di  
namespace Microsoft.Extensions.DependencyInjection
// ReSharper restore CheckNamespace
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddStealthSharpSerialization(this IServiceCollection serviceCollection,
            Action<SerializationOptions> configAction)
        {
            serviceCollection.Configure(configAction);

            serviceCollection.AddSingleton<IReflectionCache, ReflectionCache>();
            serviceCollection.AddSingleton<ICustomConverterFactory, CustomConverterFactory>();
            serviceCollection.AddSingleton<IPacketSerializer, PacketSerializer>();
            serviceCollection.AddSingleton<IMarshaler, Marshaler>();

            return serviceCollection;
        }

        public static IServiceCollection AddStealthSharpSerialization(this IServiceCollection serviceCollection)
            => AddStealthSharpSerialization(serviceCollection, opt => { });
    }
}