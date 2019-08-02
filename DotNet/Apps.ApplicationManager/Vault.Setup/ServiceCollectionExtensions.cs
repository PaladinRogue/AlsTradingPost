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

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateSharedDataKeysApplicationKernalService, CreateSharedDataKeysApplicationKernalService>();
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;

            return services
                .AddScoped<IValidator<CreateApplicationCommandDdto>, CreateApplicationValidator>()
                .AddScoped<IValidator<AddApplicationDataKeyCommandDdto>, AddApplicationDataKeyValidator>()
                .AddScoped<IValidator<CreateSharedDataKeyCommandDdto>, CreateSharedDataKeyValidator>()
                .AddScoped<IValidator<ChangeSharedDataKeyCommandDdto>, ChangeSharedDataKeyValidator>();
        }

        public static IServiceCollection RegisterDomainCommands(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>()
                .AddScoped<IAddApplicationDataKeyCommand, AddApplicationDataKeyCommand>()
                .AddScoped<ICreateSharedDataKeyCommand, CreateSharedDataKeyCommand>()
                .AddScoped<IChangeSharedDataKeyCommand, ChangeSharedDataKeyCommand>();
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMasterKeyProvider, MasterKeyProvider>()
                .AddSingletonCache<IDataKeyProvider, DataKeyProvider, ICacheDecorator<string, DataKey>, DataKeyProviderCacheDecorator, VaultCacheService>()
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }

        public static IServiceCollection RegisterPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICommandRepository<Application>, CommandRepository<Application>>();
            services.AddScoped<ICommandRepository<SharedDataKey>, CommandRepository<SharedDataKey>>();

            services.AddScoped<IQueryRepository<Application>, QueryRepository<Application>>();
            services.AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey>>();

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<VaultDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<VaultDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            return services;
        }
    }
}