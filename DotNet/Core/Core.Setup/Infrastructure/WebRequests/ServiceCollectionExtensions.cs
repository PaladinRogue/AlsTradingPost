using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Application.WebRequests;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.WebRequests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseWebRequests(this IServiceCollection services)
        {
            return services
                .AddSingleton<IHttpJson, HttpClientFactory>()
                .AddSingleton<IHttpString, HttpClientFactory>()
                .AddSingleton<IHttpRequest, HttpClientFactory>();
        }
    }
}