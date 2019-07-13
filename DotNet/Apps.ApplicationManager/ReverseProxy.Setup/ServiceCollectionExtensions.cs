using Microsoft.Extensions.DependencyInjection;
using ReverseProxy.ApplicationServices.Applications;
using ReverseProxy.Setup.Infrastructure.Applications;

namespace ReverseProxy.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterReverseProxyServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<IApplicationKernalService, ApplicationKernalService>()
                .AddSingleton<IApplicationCache, InMemoryApplicationCache>();
        }
    }
}