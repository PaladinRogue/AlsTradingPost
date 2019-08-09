using Common.Application.WebRequests;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.WebRequests
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