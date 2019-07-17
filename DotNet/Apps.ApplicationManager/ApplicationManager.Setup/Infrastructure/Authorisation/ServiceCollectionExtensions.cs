using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApplicationManager.Setup.Infrastructure.Caching;
using Common.ApplicationServices.Authentication;
using Common.Authorisation;
using Common.Authorisation.Policies;
using Common.Authorisation.Policies.Json;
using Common.Authorisation.Restrictions;
using Common.Resources.Settings;
using Common.Setup.Infrastructure.Authorisation;
using Common.Setup.Infrastructure.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using CommonJwtClaimIdentifiers = Common.ApplicationServices.Claims.Constants.JwtClaimIdentifiers;
using CommonJwtClaims = Common.ApplicationServices.Authentication.Constants.JwtClaims;
using CommonPolicies = Common.Api.Authorisation.Policies;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseJsonPolicyAuthorisation(this IServiceCollection services,
            IConfiguration configuration)
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
                ValidAudiences = new List<string>
                {
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
                options.AddPolicy(CommonPolicies.AppAccess, policy => policy.RequireClaim(CommonJwtClaimIdentifiers.Rol, CommonJwtClaims.AppAccess));
                options.AddPolicy(CommonPolicies.RestrictedAppAccess, policy => policy.RequireClaim(CommonJwtClaimIdentifiers.Rol, CommonJwtClaims.RestrictedAppAccess));
                options.AddPolicy(Policies.User, policy => policy.Requirements.Add(new UserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, IsUserAuthorizationHandler>();

            services.AddScoped<IJwtFactory, JwtFactory>();

            services.AddSingleton<ICurrentIdentityProvider, CurrentIdentityProvider>();
            services.AddScopedCache<IAuthorisationPolicy, JsonAuthorisationPolicy, AuthorisationPolicyCacheDecorator, IdentityCacheService>(ServiceLifetime.Singleton);
            services.AddSingleton<ISelfProvider, SelfIdentityProvider>();
            services.AddSingleton<IAuthorisationRestrictionProvider, AuthorisationRestrictionProvider>();
            services.AddSingleton<IJsonAuthorisationPolicyProvider>(s => new JsonAuthorisationPolicyProvider(
                JObject.Parse(File.ReadAllText("authorisationPolicy.json")
                )));

            services.AddScoped<IAuthorisationRestriction, UserAuthorisationRestriction>();

            return services;
        }
    }
}