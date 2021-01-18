using StealthSharp.Serialization;

// ReSharper disable CheckNamespace Microsoft DI Extension methods recommend to place in Microsoft namespace https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage#register-services-for-di  
namespace Microsoft.Extensions.DependencyInjection
// ReSharper restore CheckNamespace
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddStealthSharpSerialization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPacketSerializer, PacketSerializer>();
            serviceCollection.AddSingleton<IPacketDeserializer, PacketDeserializer>();

            return serviceCollection;
        }
    }
}