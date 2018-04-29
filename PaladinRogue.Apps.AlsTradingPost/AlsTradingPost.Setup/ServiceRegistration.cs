using AlsTradingPost.Application.Admin;
using AlsTradingPost.Application.Admin.Interfaces;
using AlsTradingPost.Application.Authentication;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.ItemReferenceData;
using AlsTradingPost.Application.ItemReferenceData.Interfaces;
using AlsTradingPost.Application.ItemReferenceData.Models;
using AlsTradingPost.Application.ItemReferenceData.Validators;
using AlsTradingPost.Domain.AdminDomain;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain;
using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Persistence;
using AlsTradingPost.Persistence.Repositories;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.Authorization;
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
using Common.Application.Encryption;
using Common.Application.Encryption.Interfaces;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Setup.Infrastructure.Authorization;
using Common.Setup.Infrastructure.Transactions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Transactions;

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

            services.AddTransient<IValidator<ItemReferenceDataSearchAdto>, ItemReferenceDataSearchAdtoValidator>();
        }

        public static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped<IAuthenticationApplicationService, AuthenticationApplicationService>();

            services.AddScoped<IAdminApplicationService, AdminApplicationService>();

            services.AddScoped<IItemReferenceDataApplicationService, ItemReferenceDataApplicationService>();
        }

        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));
            services.AddScoped<IAuditCommandService, AuditCommandService>();

            services.AddScoped<IUserDomainService, UserDomainService>();
            services.AddScoped<IUserCommandService, UserCommandService>();
            services.AddScoped<IUserQueryService, UserQueryService>();

            services.AddScoped<IAdminCommandService, AdminCommandService>();
            services.AddScoped<IAdminQueryService, AdminQueryService>();

            services.AddScoped<IItemReferenceDataQueryService, ItemReferenceDataQueryService>();
        }

        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemReferenceDataRepository, ItemReferenceDataRepository>();

            services.AddDbContext<AlsTradingPostDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AlsTradingPostDbContext>());
            services.AddTransient<ITransactionFactory, TransactionFactory>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentIdentityProvider, CurrentIdentityProvider>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
            services.AddSingleton<IRouteProvider<PersonaFlags>, PersonaRouteProvider>();
        }
    }
}
