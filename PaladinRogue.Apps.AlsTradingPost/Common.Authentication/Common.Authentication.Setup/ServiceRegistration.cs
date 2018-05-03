using Common.Authentication.Domain.SessionDomain;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Resources.RefreshTokens;
using Common.Authentication.Setup.Infrastructure.RefreshTokens;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Authentication.Setup
{
    public static class ServiceRegistration
    {
        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<ISessionDomainService, SessionDomainService>();
            services.AddScoped<ISessionCommandService, SessionCommandService>();
            services.AddScoped<ISessionQueryService, SessionQueryService>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRefreshTokenProvider, RefreshTokenProvider>();
        }
    }
}