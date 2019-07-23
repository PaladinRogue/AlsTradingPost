using System.Text;
using KeyVault.Setup.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Broker.Setup.DataKeys
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMasterKey(this IServiceCollection services, IConfiguration configuration)
        {
            DataProtectionSettings dataProtectionSettings = new DataProtectionSettings();
            IConfigurationSection dataProtectionSettingsSection = configuration.GetSection(nameof(DataProtectionSettings));

            dataProtectionSettingsSection.Bind(dataProtectionSettings);

            SymmetricSecurityKey masterSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(dataProtectionSettings.MasterKey));

            return services.Configure<DataProtectionSettings>(options =>
            {
                options.MasterSecurityKey = masterSecurityKey;
            });
        }
    }
}