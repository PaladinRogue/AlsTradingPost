using System;
using System.Text;
using Common.Api.Authentication;
using Common.Api.Authentication.Constants;
using Common.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AlsTradingPost.Setup
{
    public class JwtRegistration
    {
        public static void RegisterOptions(IConfiguration configuration, IServiceCollection services)
		{
            AppSettings appSettings = new AppSettings();
		    IConfigurationSection appSettingsSection = configuration.GetSection(nameof(AppSettings));
            appSettingsSection.Bind(appSettings);

            JwtIssuerOptions jwtIssuerOptions = new JwtIssuerOptions();
            IConfigurationSection jwtIssuerOptionsSection = configuration.GetSection(nameof(JwtIssuerOptions));

		    SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtIssuerOptions.Issuer;
				options.Audience = jwtIssuerOptions.Audience;
				options.SigningKey = signingKey;
			    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

		    jwtIssuerOptionsSection.Bind(jwtIssuerOptions);
            
		    services.AddScoped<IJwtFactory, JwtFactory>();

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
		    {
		        ValidateIssuer = true,
		        ValidIssuer = jwtIssuerOptions.Issuer,

		        ValidateAudience = true,
		        ValidAudience = jwtIssuerOptions.Audience,

		        ValidateIssuerSigningKey = true,
		        IssuerSigningKey = signingKey,

		        RequireExpirationTime = false,
		        ValidateLifetime = true,
		        ClockSkew = TimeSpan.Zero
		    };

		    services.AddAuthentication(options =>
		    {
		        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

		    }).AddJwtBearer(configureOptions =>
		    {
		        configureOptions.ClaimsIssuer = jwtIssuerOptions.Issuer;
		        configureOptions.TokenValidationParameters = tokenValidationParameters;
		        configureOptions.SaveToken = true;
		    });
            
		    services.AddAuthorization(options =>
		    {
		        options.AddPolicy("AppAccess", policy => policy.RequireClaim(JwtClaimIdentifiers.Rol, JwtClaims.AppAccess));
		    });
        }
    }
}
