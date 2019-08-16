using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaladinRogue.Authentication.Setup.Infrastructure.SystemAdmins
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadSystemAdminIdentitySettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<SystemAdminIdentitySettings>(configuration.GetSection(nameof(SystemAdminIdentitySettings)));
        }
    }
}