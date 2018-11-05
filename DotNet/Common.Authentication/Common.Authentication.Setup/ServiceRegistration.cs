using Common.Authentication.Resources.RefreshTokens;
using Common.Authentication.Setup.Infrastructure.RefreshTokens;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Authentication.Setup
{
    public static class ServiceRegistration
    {
        public static void RegisterDomainServices(IServiceCollection services)
        {
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRefreshTokenProvider, RefreshTokenProvider>();
        }
    }
}