using System.Text;
using Common.ApplicationServices.Caching;
using Common.Domain.DataProtectors;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using Libs.Vault.Broker.ApplicationServices;
using Libs.Vault.Domain.Caching;
using Libs.Vault.Domain.Domain.Persistence;
using Libs.Vault.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.EntityFramework.Repositories;
using Vault.Domain.Applications;
using Vault.Domain.Applications.AddDataKey;
using Vault.Domain.Applications.Create;
using Vault.Domain.SharedDataKeys;
using Vault.Persistence;
using Vault.Setup.Infrastructure.DataKeys;
using Vault.Setup.Infrastructure.Settings;

namespace Vault.Broker.Setup.DataKeys
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
                .AddScoped<ICommandRepository<Application>, CommandRepository<Application, VaultDbContext>>()
                .AddScoped<IQueryRepository<Application>, QueryRepository<Application, VaultDbContext>>()
                .AddScoped<IQueryRepository<SharedDataKey>, QueryRepository<SharedDataKey, VaultDbContext>>();
        }
    }
}