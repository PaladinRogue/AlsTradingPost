using Common.Domain.DataProtectors;
using Common.Resources.Encryption;
using Common.Setup.Infrastructure.Encryption;
using Common.Setup.Infrastructure.Hashing;
using Microsoft.Extensions.DependencyInjection;
using DataProtector = Common.Domain.DataProtectors.DataProtector;

namespace Common.Setup.Infrastructure.DataProtection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDataProtection(
            this IServiceCollection services)
        {
            return services
                .AddSingleton<IEncryptionFactory, AesEncryptionFactory>()
                .AddSingleton<IHashFactory, Sha256HashFactory>()
                .AddSingleton<IDataProtector, DataProtector>()
                .AddSingleton<IDataHasher, DataHasher>();
        }
    }
}