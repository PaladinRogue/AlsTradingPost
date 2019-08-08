using Authorisation.Application.Policies.Allow;
using Authorisation.Application.Policies.Deny;
using Microsoft.Extensions.DependencyInjection;

namespace Authorisation.Application.Policies
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