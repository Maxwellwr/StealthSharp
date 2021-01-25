using Microsoft.Extensions.Configuration;
using StealthSharp.Serialization;

// ReSharper disable CheckNamespace Microsoft DI Extension methods recommend to place in Microsoft namespace https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage#register-services-for-di  
namespace Microsoft.Extensions.DependencyInjection
// ReSharper restore CheckNamespace
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddStealthSharpSerialization(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.Configure<SerializationOptions>(
                configuration.GetSection(SerializationOptions.ConfigSection));

            return serviceCollection.AddStealthSharpSerialization();
        }
        
        public static IServiceCollection AddStealthSharpSerialization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBitConvert, BitConvert>();
            serviceCollection.AddSingleton<IReflectionCache, ReflectionCache>();
            serviceCollection.AddTransient<IPacketSerializer, PacketSerializer>();

            return serviceCollection;
        }
    }
}