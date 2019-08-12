using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Authorisation.Application.ApplicationServices;
using PaladinRogue.Library.Authorisation.Common.Managers;
using PaladinRogue.Library.Authorisation.Common.Policies;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Managers;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Owners;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Deny;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Restrictions;

namespace PaladinRogue.Library.Authorisation.Setup
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