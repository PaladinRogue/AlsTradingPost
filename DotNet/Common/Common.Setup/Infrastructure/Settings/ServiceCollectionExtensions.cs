using Common.Resources.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Settings
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadAppSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        }

        public static IServiceCollection LoadHostSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<HostSettings>(configuration);
        }

        public static IServiceCollection LoadSystemAdminIdentitySettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<SystemAdminIdentitySettings>(configuration.GetSection(nameof(SystemAdminIdentitySettings)));
        }
    }
}