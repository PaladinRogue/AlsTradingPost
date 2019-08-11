using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PaladinRogue.Libray.Core.Application.Caching;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Core.Domain.Persistence;
using PaladinRogue.Libray.Persistence.EntityFramework.Repositories;
using PaladinRogue.Libray.Persistence.Setup.Infrastructure.Caching;
using PaladinRogue.Libray.Vault.Application.Caching;
using PaladinRogue.Libray.Vault.Application.DataKeys;
using PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey;
using PaladinRogue.Libray.Vault.Domain.Applications.Create;
using PaladinRogue.Libray.Vault.Domain.DataKeys.Persistence;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys;
using PaladinRogue.Libray.Vault.Persistence;
using PaladinRogue.Libray.Vault.Persistence.DataKeys;
using PaladinRogue.Libray.Vault.Setup.Infrastructure.DataKeys;
using PaladinRogue.Libray.Vault.Setup.Infrastructure.Settings;
using PaladinRogue.Libray.Vault.Setup.Infrastructure.Transactions;

namespace PaladinRogue.Libray.Vault.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMasterKey(this IServiceCollection services, IConfiguration configuration)
        {
            DataProtectionSettings dataProtectionSettings = new DataProtectionSettings();
            IConfigurationSection dataProtectionSettingsSection = configuration.GetSection(nameof(DataProtectionSettings));

            dataProtectionSettingsSection.Bind(dataProtectionSettings);

            SymmetricSecurityKey masterSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(dataProtectionSettings.MasterKey));

            return services.Configure<DataProtectionSettings>(options =>
            {
                options.MasterSecurityKey = masterSecurityKey;
            });
        }

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
                .AddSingletonCache<IDataKeyProvider, DataKeyProvider, ICacheDecorator<string, DataKey>, DataKeyProviderCacheDecorator, ApplicationCacheService>()

                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>()
                .AddScoped<IValidator<CreateApplicationCommandDdto>, CreateApplicationValidator>()

                .AddScoped<IAddApplicationDataKeyCommand, AddApplicationDataKeyCommand>()
                .AddScoped<IValidator<AddApplicationDataKeyCommandDdto>, AddApplicationDataKeyValidator>()

                .AddScoped<IVaultTransactionManager, VaultTransactionManager>()
                .AddScoped<IDataKeyRepository, DataKeyRepository>()
                .AddScoped<ICommandRepository<Domain.Applications.Application>, CommandRepository<Domain.Applications.Application, VaultDbContext>>()
                .AddScoped<IQueryRepository<Domain.Applications.Application>, QueryRepository<Domain.Applications.Application, VaultDbContext>>()
                .AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey, VaultDbContext>>();
        }
    }
}