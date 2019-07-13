﻿using Common.ApplicationServices.Services.Query;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Manager;
using Common.Authorisation.Policies;
using Common.Authorisation.Policies.Deny;
using Common.Authorisation.Restrictions;
using Common.Domain.DataProtection;
using Common.Resources.Encryption;
using Common.Setup.Infrastructure.Encryption;
using Common.Setup.Infrastructure.Hashing;
using Common.Setup.Infrastructure.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;

namespace Common.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultMvcOptions(this IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IServiceCollection RegisterCommonServices(this IServiceCollection services)
        {
            return services.AddSingleton<IEncryptionFactory, AesEncryptionFactory>()
                .AddSingleton<IHashFactory, Sha256HashFactory>()
                .AddScoped(typeof(IQueryService<>), typeof(QueryService<>));
        }

        public static IServiceCollection RegisterCommonProviders(this IServiceCollection services)
        {
            return services.AddSingleton<IResourceOwnerProviderCollection, ResourceOwnerProviderCollection>()
                .AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IServiceCollection RegisterAuthorisationServices(this IServiceCollection services)
        {
            return services.AddSingleton<IAuthorisationRestrictionProvider, AuthorisationRestrictionProvider>()
                .AddSingleton<IAuthorisationPolicy, AlwaysDenyAuthorisationPolicy>()
                .AddScoped<ISecurityApplicationService, DefaultSecurityApplicationService>()
                .AddSingleton<IAuthorisationManager, AuthorisationManager>();
        }

        public static IServiceCollection UseSystemClock(this IServiceCollection services)
        {
            return services.AddSingleton<IClock>(SystemClock.Instance);
        }
    }
}