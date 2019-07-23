using Common.ApplicationServices.Caching;
using Common.Domain.DataProtectors;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using KeyVault.Broker.ApplicationServices;
using KeyVault.Broker.Caching;
using KeyVault.Broker.Domain.Persistence;
using KeyVault.Broker.Persistence;
using KeyVault.Domain.Applications;
using KeyVault.Domain.Applications.AddDataKey;
using KeyVault.Domain.Applications.Create;
using KeyVault.Domain.SharedDataKeys;
using KeyVault.Persistence;
using KeyVault.Setup.Infrastructure.DataKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Repositories;
using DataKeyProvider = KeyVault.Broker.Setup.DataKeys.DataKeyProvider;
using DataKeyProviderCacheDecorator = KeyVault.Broker.Setup.DataKeys.DataKeyProviderCacheDecorator;

namespace KeyVault.Broker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseKeyVault(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<KeyVaultDbContext>(options =>
                    options
                        .UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Vault"))
                );

            return services
                .AddScoped<IMasterKeyProvider, MasterKeyProvider>()
                .AddScoped<ICacheDataKeyApplicationKernalService, CacheDataKeyApplicationKernalService>()
                .AddSingletonCache<IDataKeyProvider, DataKeyProvider, ICacheDecorator<string, DataKey>, DataKeyProviderCacheDecorator, ApplicationCacheService>()
                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>()
                .AddScoped<IValidator<CreateApplicationCommandDdto>, CreateApplicationValidator>()
                .AddScoped<IAddApplicationDataKeyCommand, AddApplicationDataKeyCommand>()
                .AddScoped<IValidator<AddApplicationDataKeyCommandDdto>, AddApplicationDataKeyValidator>()
                .AddScoped<IDataKeyRepository, DataKeyRepository>()
                .AddScoped<ICommandRepository<Application>, CommandRepository<Application, KeyVaultDbContext>>()
                .AddScoped<IQueryRepository<Application>, QueryRepository<Application, KeyVaultDbContext>>()
                .AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey, KeyVaultDbContext>>();
        }
    }
}