using Common.Authorisation.Policies.Allow;
using Common.Authorisation.Policies.Deny;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Authorisation.Policies
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