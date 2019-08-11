using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Common.Encryption;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Encryption;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Hashing;
using DataProtector = PaladinRogue.Libray.Core.Domain.DataProtectors.DataProtector;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.DataProtection
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