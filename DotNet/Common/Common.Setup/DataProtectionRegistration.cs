using System.Text;
using Common.Domain.DataProtection;
using Common.Domain.Models.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using DataProtector = Common.Domain.DataProtection.DataProtector;

namespace Common.Setup
{
    public static class DataProtectionRegistration
    {
	    public static void Register(IConfiguration configuration, IServiceCollection services)
	    {
		    services.AddSingleton<IDataProtector, DataProtector>();
		    services.AddSingleton<IDataHasher, DataHasher>();

		    DataProtectionSettings dataProtectionSettings = new DataProtectionSettings();
	        IConfigurationSection dataProtectionSettingsSection = configuration.GetSection(nameof(DataProtectionSettings));
		    dataProtectionSettingsSection.Bind(dataProtectionSettings);

	        SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(dataProtectionSettings.Secret));

	        services.Configure<DataProtectionSettings>(options =>
	        {
	            options.SigningKey = signingKey;
	        });
	    }
    }
}
