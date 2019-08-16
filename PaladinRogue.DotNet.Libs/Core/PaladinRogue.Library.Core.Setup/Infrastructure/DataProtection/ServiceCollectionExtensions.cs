using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Common.Encryption;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Core.Setup.Infrastructure.Encryption;
using PaladinRogue.Library.Core.Setup.Infrastructure.Hashing;
using DataProtector = PaladinRogue.Library.Core.Domain.DataProtectors.DataProtector;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.DataProtection
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