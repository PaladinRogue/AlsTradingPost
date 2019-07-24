using System;
using System.Collections.Generic;
using System.IO;
using Authentication.Setup.Infrastructure.Caching;
using Common.ApplicationServices.Authentication;
using Common.Authorisation;
using Common.Authorisation.Policies;
using Common.Authorisation.Policies.Json;
using Common.Authorisation.Restrictions;
using Common.Domain.DataProtectors;
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

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseJsonPolicyAuthorisation(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddJwtAuthentication(services, configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(CommonPolicies.AppAccess, policy => policy.RequireClaim(CommonJwtClaimIdentifiers.Rol, CommonJwtClaims.AppAccess));
                options.AddPolicy(CommonPolicies.RestrictedAppAccess, policy => policy.RequireClaim(CommonJwtClaimIdentifiers.Rol, CommonJwtClaims.RestrictedAppAccess));
                options.AddPolicy(Policies.User, policy => policy.Requirements.Add(new UserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, IsUserAuthorizationHandler>();

            services.AddScoped<IJwtFactory, JwtFactory>();

            services.AddSingleton<ICurrentIdentityProvider, CurrentIdentityProvider>();
            services.AddScopedCache<IAuthorisationPolicy, JsonAuthorisationPolicy, AuthorisationPolicyCacheDecorator, IdentityCacheService>();
            services.AddSingleton<ISelfProvider, SelfIdentityProvider>();
            services.AddSingleton<IAuthorisationRestrictionProvider, AuthorisationRestrictionProvider>();
            services.AddSingleton<IJsonAuthorisationPolicyProvider>(s => new JsonAuthorisationPolicyProvider(
                JObject.Parse(File.ReadAllText("authorisationPolicy.json"))
            ));

            services.AddScoped<IAuthorisationRestriction, UserAuthorisationRestriction>();

            return services;
        }

        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            JwtIssuerOptions jwtIssuerOptions = new JwtIssuerOptions();
            IConfigurationSection jwtIssuerOptionsSection = configuration.GetSection(nameof(JwtIssuerOptions));

            jwtIssuerOptionsSection.Bind(jwtIssuerOptions);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDataProtector>().SetDataProtector();
                IDataKeyProvider dataKeyProvider = scope.ServiceProvider.GetRequiredService<IDataKeyProvider>();

                DataKey dataKey = dataKeyProvider.GetAsync(SharedDataKeys.Authentication).Result;

                services.Configure<JwtIssuerOptions>(options =>
                {
                    options.Issuer = jwtIssuerOptions.Issuer;
                    options.Audience = jwtIssuerOptions.Audience;
                    options.SigningKey = dataKey.Value;
                    options.SigningCredentials = new SigningCredentials(dataKey.Value, SecurityAlgorithms.HmacSha256);
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
                        dataKey.Value
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
            }
        }
    }
}