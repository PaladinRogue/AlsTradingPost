using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Authorisation.Application.ApplicationServices;
using PaladinRogue.Libray.Authorisation.Common.Managers;
using PaladinRogue.Libray.Authorisation.Common.Policies;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Managers;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Owners;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies.Deny;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Restrictions;

namespace PaladinRogue.Libray.Authorisation.Setup
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