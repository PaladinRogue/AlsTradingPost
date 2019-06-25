using System;
using System.Collections.Generic;
using System.Text;
using ApplicationManager.ApplicationServices.Claims;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using Common.ApplicationServices.Authentication;
using Common.Setup.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CommonJwtClaimIdentifiers = Common.ApplicationServices.Claims.Constants.JwtClaimIdentifiers;
using CommonJwtClaims = Common.Api.Authentication.Constants.JwtClaims;

namespace ApplicationManager.Setup
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

            jwtIssuerOptionsSection.Bind(jwtIssuerOptions);

		    SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtIssuerOptions.Issuer;
				options.Audience = jwtIssuerOptions.Audience;
				options.SigningKey = signingKey;
			    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

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
		        RequireExpirationTime = true,
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
		        options.AddPolicy(CommonJwtClaims.AppAccess, policy => policy.RequireClaim(CommonJwtClaimIdentifiers.Rol, CommonJwtClaims.AppAccess));
		        options.AddPolicy(CommonJwtClaims.RestrictedAppAccess, policy => policy.RequireClaim(CommonJwtClaimIdentifiers.Rol, CommonJwtClaims.RestrictedAppAccess));
		        options.AddPolicy(JwtClaims.IsUser, policy => policy.Requirements.Add(new IsUserRequirement()));
		    });

            services.AddSingleton<IAuthorizationHandler, IsUserAuthorizationHandler>();

			services.AddScoped<IJwtFactory, JwtFactory>();
		}
    }
}
