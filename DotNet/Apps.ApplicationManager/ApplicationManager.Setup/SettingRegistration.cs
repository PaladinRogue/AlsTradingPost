using ApplicationManager.Setup.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class SettingRegistration
    {
        public static void RegisterSystemAdminIdentitySettings(IConfiguration configuration, IServiceCollection services)
        {
            SystemAdminIdentitySettings systemAdminIdentitySettings = new SystemAdminIdentitySettings();
            IConfigurationSection systemAdminIdentitySettingsSection = configuration.GetSection(nameof(SystemAdminIdentitySettings));
            systemAdminIdentitySettingsSection.Bind(systemAdminIdentitySettings);
        }
    }
}