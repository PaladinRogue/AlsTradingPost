using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Common.Settings;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Settings
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
    }
}