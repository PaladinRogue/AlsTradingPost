using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Application.Caching;
using PaladinRogue.Libray.Core.Application.Transactions;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Core.Domain.Persistence;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Routing;
using PaladinRogue.Libray.Persistence.EntityFramework.Repositories;
using PaladinRogue.Libray.Persistence.Setup.Infrastructure.Caching;
using PaladinRogue.Libray.Persistence.Setup.Infrastructure.Transactions;
using PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey;
using PaladinRogue.Libray.Vault.Domain.Applications.Create;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Change;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Create;
using PaladinRogue.Libray.Vault.Persistence;
using PaladinRogue.Libray.Vault.Setup.Infrastructure.DataKeys;
using PaladinRogue.Vault.Application.SharedDataKeys.Create;
using PaladinRogue.Vault.Setup.Infrastructure.Caching;
using DataKeyProvider = PaladinRogue.Vault.Setup.Infrastructure.DataKeys.DataKeyProvider;
using DataKeyProviderCacheDecorator = PaladinRogue.Vault.Setup.Infrastructure.DataKeys.DataKeyProviderCacheDecorator;

namespace PaladinRogue.Vault.Setup
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
                .AddScoped<ICommandRepository<Libray.Vault.Domain.Applications.Application>, CommandRepository<Libray.Vault.Domain.Applications.Application>>()
                .AddScoped<IQueryRepository<Libray.Vault.Domain.Applications.Application>, QueryRepository<Libray.Vault.Domain.Applications.Application>>();
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