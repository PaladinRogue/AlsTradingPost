using AlsTradingPost.Application.AdminApplication;
using AlsTradingPost.Application.AdminApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication;
using AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Application.ItemReferenceDataApplication.Validators;
using AlsTradingPost.Application.UserApplication;
using AlsTradingPost.Application.UserApplication.Interfaces;
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
using AlsTradingPost.Resources.Providers;
using AlsTradingPost.Resources.Providers.Interfaces;
using Common.Api.Encryption;
using Common.Api.Encryption.Interfaces;
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Application.Identity;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Resources.Concurrency.Interfaces;
using Common.Resources.Transactions;
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
        public static void RegisterValidators(IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;
            
            services.AddTransient<IValidator<ItemReferenceDataSearchAdto>, ItemReferenceDataSearchAdtoValidator>();
        }

        public static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped<IUserApplicationService, UserApplicationService>();

            services.AddScoped<IAdminApplicationService, AdminApplicationService>();

            services.AddScoped<IItemReferenceDataApplicationService, ItemReferenceDataApplicationService>();
        }

        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));
            services.AddScoped<IAuditCommandService, AuditCommandService>();

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

            services.AddDbContext<AlsTradingPostDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AlsTradingPostDbContext>());
            services.AddTransient<ITransactionFactory, TransactionFactory>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentIdentityProvider, CurrentIdentityProvider>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}
