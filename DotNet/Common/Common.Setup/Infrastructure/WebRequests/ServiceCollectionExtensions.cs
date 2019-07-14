using Common.ApplicationServices.WebRequests;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.WebRequests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseWebRequests(this IServiceCollection services)
        {
            return services
                .AddSingleton<IHttpClientFactory, HttpClientFactory>();
        }
    }
}