using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PaladinRogue.Library.Core.Application.Caching;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Core.Domain.Persistence;
using PaladinRogue.Library.Persistence.EntityFramework.Repositories;
using PaladinRogue.Library.Persistence.Setup.Infrastructure.Caching;
using PaladinRogue.Library.Vault.Application.Caching;
using PaladinRogue.Library.Vault.Application.DataKeys;
using PaladinRogue.Library.Vault.Domain.Applications.AddDataKey;
using PaladinRogue.Library.Vault.Domain.Applications.Create;
using PaladinRogue.Library.Vault.Domain.DataKeys.Persistence;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys;
using PaladinRogue.Library.Vault.Persistence;
using PaladinRogue.Library.Vault.Persistence.DataKeys;
using PaladinRogue.Library.Vault.Setup.Infrastructure.DataKeys;
using PaladinRogue.Library.Vault.Setup.Infrastructure.Settings;
using PaladinRogue.Library.Vault.Setup.Infrastructure.Transactions;

namespace PaladinRogue.Library.Vault.Setup
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