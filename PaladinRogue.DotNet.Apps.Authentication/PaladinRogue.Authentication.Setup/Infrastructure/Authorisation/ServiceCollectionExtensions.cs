using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PaladinRogue.Authentication.Setup.Infrastructure.Caching;
using PaladinRogue.Library.Authorisation.Common.Policies;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Json;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Restrictions;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Self;
using PaladinRogue.Library.Core.Application.Authentication;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Core.Setup.Infrastructure.Authorisation;
using PaladinRogue.Library.Persistence.Setup.Infrastructure.Caching;
using CommonJwtClaimIdentifiers = PaladinRogue.Library.Core.Application.Claims.Constants.JwtClaimIdentifiers;
using CommonJwtClaims = PaladinRogue.Library.Core.Application.Authentication.Constants.JwtClaims;
using CommonPolicies = PaladinRogue.Library.Core.Setup.Infrastructure.Authorisation.Policies;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
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
            services.AddScopedCache<IAuthorisationPolicy, AuthorisationPolicy, AuthorisationPolicyCacheDecorator, IdentityCacheService>();
            services.AddScoped<ISelfProvider, SelfIdentityProvider>();
            services.AddScoped<IAuthorisationRestrictionProvider, AuthorisationRestrictionProvider>();
            services.AddSingleton<IAuthorisationPolicyProvider>(s => new JsonAuthorisationPolicyProvider(
                JObject.Parse(File.ReadAllText("authorisationPolicy.json"))
            ));

            services.AddScoped<IAuthorisationRestriction, UserAuthorisationRestriction>();
            services.AddScoped<IAuthorisationRestriction, RestrictedAppAccessAuthorisationRestriction>();

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