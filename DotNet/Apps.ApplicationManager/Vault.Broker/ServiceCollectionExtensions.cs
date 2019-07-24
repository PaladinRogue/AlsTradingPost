using Common.ApplicationServices.Caching;
using Common.Domain.DataProtectors;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using Vault.Domain.Applications;
using Vault.Domain.Applications.AddDataKey;
using Vault.Domain.Applications.Create;
using Vault.Domain.SharedDataKeys;
using Vault.Persistence;
using Vault.Setup.Infrastructure.DataKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Repositories;
using Vault.Broker.ApplicationServices;
using Vault.Broker.Caching;
using Vault.Broker.Domain.Persistence;
using Vault.Broker.Persistence;

namespace Vault.Broker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseVault(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddOptions()
                .AddDbContext<VaultDbContext>(options =>
                    options
                        .UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Vault"))
                );

            return services
                .AddScoped<IMasterKeyProvider, MasterKeyProvider>()
                .AddScoped<ICacheDataKeyApplicationKernalService, CacheDataKeyApplicationKernalService>()
                .AddSingletonCache<IDataKeyProvider, Setup.DataKeys.DataKeyProvider, ICacheDecorator<string, DataKey>, Setup.DataKeys.DataKeyProviderCacheDecorator, ApplicationCacheService>()

                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>()
                .AddScoped<IValidator<CreateApplicationCommandDdto>, CreateApplicationValidator>()

                .AddScoped<IAddApplicationDataKeyCommand, AddApplicationDataKeyCommand>()
                .AddScoped<IValidator<AddApplicationDataKeyCommandDdto>, AddApplicationDataKeyValidator>()

                .AddScoped<IVaultTransactionManager, VaultTransactionManager>()
                .AddScoped<IDataKeyRepository, DataKeyRepository>()
                .AddScoped<ICommandRepository<Application>, CommandRepository<Application, VaultDbContext>>()
                .AddScoped<IQueryRepository<Application>, QueryRepository<Application, VaultDbContext>>()
                .AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey, VaultDbContext>>();
        }
    }
}