using System;
using Microsoft.Extensions.Configuration;
using StealthSharp.Network;

// ReSharper disable CheckNamespace Microsoft DI Extension methods recommend to place in Microsoft namespace https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage#register-services-for-di  
namespace Microsoft.Extensions.DependencyInjection
// ReSharper restore CheckNamespace
{
    public static class ServiceProviderExtensions 
    {
        public static IServiceCollection AddStealthSharpClient<TId, TSize, TMapping,TMapper>(this IServiceCollection serviceCollection)
            where TMapper: class, ITypeMapper<TMapping>
            where TId: unmanaged
            where  TSize: unmanaged
            where TMapping: unmanaged
        {
            serviceCollection.AddStealthSharpSerialization();
            
            return AddServices<TId, TSize, TMapping, TMapper>(serviceCollection);
        }
        
        public static IServiceCollection AddStealthSharpClient<TId, TSize, TMapping,TMapper>(this IServiceCollection serviceCollection, Type typeMapper, IConfiguration configuration)
            where TMapper: class, ITypeMapper<TMapping>
            where TId: unmanaged
            where  TSize: unmanaged
            where TMapping: unmanaged
        {
            serviceCollection.AddStealthSharpSerialization(configuration);
            
            return AddServices<TId, TSize, TMapping, TMapper>(serviceCollection);
        }

        private static IServiceCollection AddServices<TId, TSize, TMapping, TMapper>(IServiceCollection serviceCollection)
            where TMapper: class, ITypeMapper<TMapping>
            where TId: unmanaged
            where  TSize: unmanaged
            where TMapping: unmanaged
        {
            serviceCollection
                .AddSingleton<IStealthSharpClient<TId, TSize, TMapping>, StealthSharpClient<TId, TSize, TMapping>>();
            serviceCollection.AddSingleton<ITypeMapper<TMapping>, TMapper> ();
            return serviceCollection;
        }
    }
}