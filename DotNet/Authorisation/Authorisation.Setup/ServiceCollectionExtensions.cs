using Authorisation.Application;
using Authorisation.Application.ApplicationServices;
using Authorisation.Application.Manager;
using Authorisation.Application.Policies;
using Authorisation.Application.Policies.Deny;
using Authorisation.Application.Restrictions;
using Microsoft.Extensions.DependencyInjection;

namespace Authorisation.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAuthorisationServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IResourceOwnerProviderCollection, ResourceOwnerProviderCollection>()
                .AddSingleton<IAuthorisationRestrictionProvider, AuthorisationRestrictionProvider>()
                .AddSingleton<IAuthorisationPolicy, AlwaysDenyAuthorisationPolicy>()
                .AddScoped<IAuthorisationManager, AuthorisationManager>()
                .AddScoped<ISecurityApplicationService, DefaultSecurityApplicationService>()
                .AddScoped<IClaimsApplicationKernalService, ClaimsApplicationKernalService>();
        }
    }
}