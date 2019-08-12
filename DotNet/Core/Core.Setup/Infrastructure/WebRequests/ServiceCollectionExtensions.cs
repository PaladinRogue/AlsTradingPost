using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Application.WebRequests;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.WebRequests
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