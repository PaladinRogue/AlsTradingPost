using System;
using Common.ApplicationServices.Caching;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using KeyVault.Broker.ApplicationServices;
using KeyVault.Broker.Caching;
using KeyVault.Broker.Domain;
using KeyVault.Broker.Domain.Persistence;
using KeyVault.Broker.Persistence;
using KeyVault.Domain.Applications;
using KeyVault.Domain.Applications.AddDataKey;
using KeyVault.Domain.Applications.Create;
using KeyVault.Domain.SharedDataKeys;
using KeyVault.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Repositories;

namespace KeyVault.Broker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseKeyVault<T>(this IServiceCollection services, IConfiguration configuration) where T : struct, Enum
        {
            services
                .AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<KeyVaultDbContext>(options =>
                    options
                        .UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Vault"), providerOptions =>
                            providerOptions
                                .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                            )
                );

            return services
                .AddScoped<ICacheDataKeyApplicationKernalService, CacheDataKeyApplicationKernalService>()
                .AddSingletonCache<IDataKeyProvider, DataKeyProvider, ICacheDecorator<T, DataKey<T>>, DataKeyProviderCacheDecorator<T>, ApplicationCacheService>()
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