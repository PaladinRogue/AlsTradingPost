using System;
using System.Collections.Generic;
using System.Text;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.Authorization;
using Common.Api.Authentication;
using Common.Api.Authentication.Constants;
using Common.Api.Authentication.Interfaces;
using Common.Api.Settings;
using Common.Application.Authentication;
using Common.Application.Claims.Constants;
using Common.Resources.Authentication;
using Common.Setup.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

            JwtAuthenticationIssuerOptions jwtAuthenticationIssuerOptions = new JwtAuthenticationIssuerOptions();
            IConfigurationSection jwtAuthenticationIssuerOptionsSection = configuration.GetSection(nameof(JwtAuthenticationIssuerOptions));

		    SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtIssuerOptionsSection[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtIssuerOptionsSection[nameof(JwtIssuerOptions.Audience)];
                options.SigningKey = signingKey;
			    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

		    jwtIssuerOptionsSection.Bind(jwtIssuerOptions);

		    SymmetricSecurityKey authenticationSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.AuthenticationSecret));

            services.Configure<JwtAuthenticationIssuerOptions>(options =>
            {
                options.Issuer = jwtAuthenticationIssuerOptionsSection[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAuthenticationIssuerOptionsSection[nameof(JwtIssuerOptions.Audience)];
                options.SigningKey = authenticationSigningKey;
			    options.SigningCredentials = new SigningCredentials(authenticationSigningKey, SecurityAlgorithms.HmacSha256);
            });

		    jwtAuthenticationIssuerOptionsSection.Bind(jwtAuthenticationIssuerOptions);
            
		    services.AddScoped<IJwtFactory, JwtFactory>();

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
		    {
		        ValidateIssuer = true,
		        ValidIssuers = new List<string>
		        {
		            jwtIssuerOptions.Issuer,
                    jwtAuthenticationIssuerOptions.Issuer
                },

		        ValidateAudience = true,
		        ValidAudiences = new List<string> {
		            jwtIssuerOptions.Audience,
		            jwtAuthenticationIssuerOptions.Audience
                },

		        ValidateIssuerSigningKey = true,
		        IssuerSigningKeys = new List<SecurityKey>
		        {
		            signingKey,
		            authenticationSigningKey
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
		        options.AddPolicy(JwtClaims.AppAccess, policy => policy.RequireClaim(JwtClaimIdentifiers.Rol, JwtClaims.AppAccess));
			    foreach (KeyValuePair<PersonaFlags, string> keyValuePair in PersonaPolicyMapper.GetMap())
			    {
				    options.AddPolicy(keyValuePair.Value, policy => policy.Requirements.Add(new HasPersonaRequirement(keyValuePair.Key)));
			    }
		    });
            
            services.AddSingleton<IAuthorizationHandler, HasPersonaHandler>();
        }
    }
}
