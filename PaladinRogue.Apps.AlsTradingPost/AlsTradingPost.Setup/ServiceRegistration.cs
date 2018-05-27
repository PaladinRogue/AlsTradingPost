﻿using System.IO;
using AlsTradingPost.Application.Authentication;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Application.Authentication.Validators;
using AlsTradingPost.Application.MagicItemTemplate;
using AlsTradingPost.Application.MagicItemTemplate.Interfaces;
using AlsTradingPost.Application.MagicItemTemplate.Models;
using AlsTradingPost.Application.MagicItemTemplate.Validators;
using AlsTradingPost.Application.Trader;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using AlsTradingPost.Application.Trader.Validators;
using AlsTradingPost.Domain.AdminDomain;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain;
using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.MagicItemTemplateDomain;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.TraderDomain;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Persistence;
using AlsTradingPost.Persistence.Repositories;
using AlsTradingPost.Resources;
using AlsTradingPost.Resources.Authorization;
using AlsTradingPost.Setup.Infrastructure.Authorisation;
using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Builders;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Routing;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;
using Common.Application.Transactions;
using Common.Authentication.Domain.Persistence;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Setup.Infrastructure.Authorization;
using Common.Setup.Infrastructure.Encryption;
using Common.Setup.Infrastructure.Encryption.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Persistence.EntityFramework.Infrastructure.Transactions;

namespace AlsTradingPost.Setup
{
    public class ServiceRegistration
    {
        public static void RegisterBuilders(IServiceCollection services)
        {
            services.AddSingleton<IMetaBuilder, MetaBuilder>();
            services.AddSingleton<ILinkBuilder, PersonaLinkBuilder>();
            services.AddSingleton<IBuildHelper, BuildHelper>();
            services.AddSingleton<ITemplateBuilder, TemplateBuilder>();
            services.AddSingleton<IResourceBuilder, ResourceBuilder>();
            services.AddSingleton<IResourceTemplateBuilder, ResourceTemplateBuilder>();
            services.AddSingleton(typeof(ICollectionResourceBuilder<>), typeof(CollectionResourceBuilder<>));
        }

        public static void RegisterValidators(IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;

            services.AddTransient<IValidator<MagicItemTemplateSearchAdto>, MagicItemTemplateSearchValidator>();
            services.AddTransient<IValidator<LoginAdto>, LoginValidator>();
            services.AddTransient<IValidator<RefreshTokenAdto>, RefreshTokenValidator>();
            services.AddTransient<IValidator<RegisterTraderAdto>, CreateTraderValidator>();
            services.AddTransient<IValidator<UpdateTraderAdto>, UpdateTraderValidator>();
        }

        public static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped<IAuthenticationApplicationService, AuthenticationApplicationService>();
            services.AddScoped<ISecure<IAuthenticationApplicationService>, AuthenticationSecurityApplicationService>();
            
            services.AddScoped<ITraderApplicationService, TraderApplicationService>();
            services.AddScoped<ISecure<ITraderApplicationService>, TraderSecurityApplicationService>();

            services.AddScoped<IMagicItemTemplateApplicationService, MagicItemTemplateApplicationService>();
            services.AddScoped<ISecure<IMagicItemTemplateApplicationService>, MagicItemTemplateSecurityApplicationService>();
        }

        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));

            services.AddScoped<IAuditDomainService, AuditDomainService>();
            services.AddScoped<IAuditCommandService, AuditCommandService>();

            services.AddScoped<IUserDomainService, UserDomainService>();
            services.AddScoped<IUserCommandService, UserCommandService>();
            services.AddScoped<IUserQueryService, UserQueryService>();

            services.AddScoped<IAdminDomainService, AdminDomainService>();
            services.AddScoped<IAdminQueryService, AdminQueryService>();
            
            services.AddScoped<ITraderDomainService, TraderDomainService>();
            services.AddScoped<ITraderCommandService, TraderCommandService>();
            services.AddScoped<ITraderQueryService, TraderQueryService>();

            services.AddScoped<IMagicItemTemplateDomainService, MagicItemTemplateDomainService>();
            services.AddScoped<IMagicItemTemplateQueryService, MagicItemTemplateQueryService>();
        }

        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ITraderRepository, TraderRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IMagicItemRepository, MagicItemRepository>();
            services.AddScoped<IMagicItemTemplateRepository, MagicItemTemplateRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();

            services.AddDbContext<AlsTradingPostDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AlsTradingPostDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentIdentityProvider, CurrentIdentityProvider>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
            services.AddSingleton<IRouteProvider<PersonaFlags>, PersonaRouteProvider>();
        }

        public static void RegisterAuthorisation(IServiceCollection services)
        {
            services.AddSingleton<IAuthorisationPolicy, JsonAuthorisationPolicy>();
            services.AddSingleton<IJsonAuthorisationPolicyProvider>(s => new JsonAuthorisationPolicyProvider(JObject.Parse(File.ReadAllText("authorisationPolicy.json"))));
        }
    }
}
