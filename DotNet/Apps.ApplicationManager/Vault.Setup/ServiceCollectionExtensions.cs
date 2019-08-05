using Common.Api.Routing;
using Common.ApplicationServices.Caching;
using Common.ApplicationServices.Transactions;
using Common.Domain.DataProtectors;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using Vault.ApplicationServices.SharedDataKeys.Create;
using Vault.Domain.Applications;
using Vault.Domain.Applications.AddDataKey;
using Vault.Domain.Applications.Create;
using Vault.Domain.SharedDataKeys;
using Vault.Domain.SharedDataKeys.Change;
using Vault.Domain.SharedDataKeys.Create;
using Vault.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;
using Vault.Setup.Infrastructure.Caching;
using Vault.Setup.Infrastructure.DataKeys;

namespace Vault.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDefaultRouting(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }

        public static IServiceCollection AddDataKeyProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMasterKeyProvider, MasterKeyProvider>()
                .AddSingletonCache<IDataKeyProvider, DataKeyProvider, ICacheDecorator<string, DataKey>, DataKeyProviderCacheDecorator, VaultCacheService>();
        }

        public static IServiceCollection AddSharedDataKeyDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateSharedDataKeysApplicationKernalService, CreateSharedDataKeysApplicationKernalService>()
                .AddScoped<ICreateSharedDataKeyCommand, CreateSharedDataKeyCommand>()
                .AddScoped<IChangeSharedDataKeyCommand, ChangeSharedDataKeyCommand>()
                .AddScoped<IValidator<CreateSharedDataKeyCommandDdto>, CreateSharedDataKeyValidator>()
                .AddScoped<IValidator<ChangeSharedDataKeyCommandDdto>, ChangeSharedDataKeyValidator>()
                .AddScoped<ICommandRepository<SharedDataKey>, CommandRepository<SharedDataKey>>()
                .AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey>>();
        }


        public static IServiceCollection AddApplicationDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>()
                .AddScoped<IAddApplicationDataKeyCommand, AddApplicationDataKeyCommand>()
                .AddScoped<IValidator<CreateApplicationCommandDdto>, CreateApplicationValidator>()
                .AddScoped<IValidator<AddApplicationDataKeyCommandDdto>, AddApplicationDataKeyValidator>()
                .AddScoped<ICommandRepository<Application>, CommandRepository<Application>>()
                .AddScoped<IQueryRepository<Application>, QueryRepository<Application>>();
        }

        public static IServiceCollection AddVaultPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<VaultDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")))
                .AddScoped<DbContext>(sp => sp.GetRequiredService<VaultDbContext>())
                .AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            return services;
        }
    }
}