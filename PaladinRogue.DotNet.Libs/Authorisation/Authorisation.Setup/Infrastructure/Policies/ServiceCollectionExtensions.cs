using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Authorisation.Common.Policies;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Allow;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Deny;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseAlwaysDenyAuthorisation(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAuthorisationPolicy, AlwaysDenyAuthorisationPolicy>();
        }

        public static IServiceCollection UseAlwaysAllowAuthorisation(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAuthorisationPolicy, AlwaysAllowAuthorisationPolicy>();
        }
    }
}