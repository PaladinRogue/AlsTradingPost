using System.Text;
using Common.Domain.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Common.Setup
{
    public static class DataProtectionRegistration
    {
	    public static void Register(IConfiguration configuration, IServiceCollection services)
	    {
		    services.AddSingleton<IDataProtector, DataProtector>();
		    
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
