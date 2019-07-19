using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using FluentValidation;
using KeyVault.ApplicationServices.SharedDataKeys.Create;
using KeyVault.Domain.Applications;
using KeyVault.Domain.Applications.AddDataKey;
using KeyVault.Domain.Applications.Create;
using KeyVault.Domain.SharedDataKeys;
using KeyVault.Domain.SharedDataKeys.Change;
using KeyVault.Domain.SharedDataKeys.Create;
using KeyVault.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;

namespace KeyVault.Setup
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ISharedDataKeysApplicationKernalService, SharedDataKeysApplicationKernalService>();
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

        public static IServiceCollection RegisterPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICommandRepository<Application>, CommandRepository<Application>>();
            services.AddScoped<ICommandRepository<SharedDataKey>, CommandRepository<SharedDataKey>>();

            services.AddScoped<IQueryRepository<Application>, QueryRepository<Application>>();
            services.AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey>>();

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<KeyVaultDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<KeyVaultDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            return services;
        }
    }
}