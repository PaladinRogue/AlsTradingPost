using System.Text;
using Common.Domain.DataProtectors;
using Common.Resources.Encryption;
using Common.Setup.Infrastructure.Encryption;
using Common.Setup.Infrastructure.Hashing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using DataProtector = Common.Domain.DataProtectors.DataProtector;

namespace Common.Setup.Infrastructure.DataProtection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDataProtection(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IEncryptionFactory, AesEncryptionFactory>()
                .AddSingleton<IHashFactory, Sha256HashFactory>()
                .AddSingleton<IDataProtector, DataProtector>()
                .AddSingleton<IDataHasher, DataHasher>();

            DataProtectionSettings dataProtectionSettings = new DataProtectionSettings();
            IConfigurationSection dataProtectionSettingsSection = configuration.GetSection(nameof(DataProtectionSettings));
            dataProtectionSettingsSection.Bind(dataProtectionSettings);

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(dataProtectionSettings.Secret));

            services.Configure<DataProtectionSettings>(options => { options.SigningKey = signingKey; });

            return services;
        }
    }
}