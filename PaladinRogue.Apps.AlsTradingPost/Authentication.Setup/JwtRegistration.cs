using System.Text;
using Common.Api.Authentication;
using Common.Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Setup
{
    public class JwtRegistration
    {
        public static void RegisterOptions(IConfiguration configuration, IServiceCollection services)
		{
		    AppSettings appSettings = new AppSettings();
		    IConfigurationSection appSettingsSection = configuration.GetSection(nameof(AppSettings));

		    appSettingsSection.Bind(appSettings);
		    services.Configure<AppSettings>(appSettingsSection);

            services.AddSingleton<IJwtFactory, JwtFactory>();
            
			IConfigurationSection jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

			services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
				options.SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));
			});
		}
		
    }
}
