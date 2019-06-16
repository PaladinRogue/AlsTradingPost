using System;
using System.Collections.Generic;
using System.Text;
using ApplicationManager.ApplicationServices.Claims;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using Common.Application.Authentication;
using Common.Setup.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JwtClaimIdentifiers = Common.Application.Claims.Constants.JwtClaimIdentifiers;

namespace ApplicationManager.Setup
{
    public class JwtRegistration
    {
        public static void RegisterOptions(IConfiguration configuration, IServiceCollection services)
		{
		    AppSettings appSettings = new AppSettings();
		    IConfigurationSection appSettingsSection = configuration.GetSection(nameof(AppSettings));
            appSettingsSection.Bind(appSettings);

            services.AddScoped<IJwtFactory, JwtFactory>();

			IConfigurationSection jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

		    SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
				options.SigningKey = signingKey;
			    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            JwtIssuerOptions jwtIssuerOptions = new JwtIssuerOptions();
            IConfigurationSection jwtIssuerOptionsSection = configuration.GetSection(nameof(JwtIssuerOptions));

		    jwtIssuerOptionsSection.Bind(jwtIssuerOptions);

		    services.AddScoped<IJwtFactory, JwtFactory>();

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
		    {
		        ValidateIssuer = true,
		        ValidIssuers = new List<string>
		        {
		            jwtIssuerOptions.Issuer
                },
		        ValidateAudience = true,
		        ValidAudiences = new List<string> {
		            jwtIssuerOptions.Audience
                },
		        ValidateIssuerSigningKey = true,
		        IssuerSigningKeys = new List<SecurityKey>
		        {
		            signingKey
                },
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
		        configureOptions.TokenValidationParameters = tokenValidationParameters;
		        configureOptions.SaveToken = true;
		    });

		    services.AddAuthorization(options =>
		    {
		        options.AddPolicy(Common.Api.Authentication.Constants.JwtClaims.AppAccess, policy => policy.RequireClaim(JwtClaimIdentifiers.Rol, Common.Api.Authentication.Constants.JwtClaims.AppAccess));
		        options.AddPolicy(JwtClaims.IsUser, policy => policy.Requirements.Add(new IsUserRequirement()));
		    });

            services.AddSingleton<IAuthorizationHandler, IsUserAuthorizationHandler>();
		}
    }
}
