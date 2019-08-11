using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Vault.Domain;
using PaladinRogue.Libray.Vault.Setup.Infrastructure.Settings;
using DataKey = PaladinRogue.Libray.Core.Domain.DataProtectors.DataKey;

namespace PaladinRogue.Libray.Vault.Setup.Infrastructure.DataKeys
{
    public class MasterKeyProvider : IMasterKeyProvider
    {
        private readonly DataProtectionSettings _dataProtectionSettings;

        public MasterKeyProvider(
            IOptions<DataProtectionSettings> dataProtectionSettingsAccessor)
        {
            _dataProtectionSettings = dataProtectionSettingsAccessor.Value;
        }

        public Task<DataKey> GetAsync()
        {
            return Task.FromResult(new DataKey
            {
                Name = MasterDataKeys.Master,
                Value = _dataProtectionSettings.MasterSecurityKey
            });
        }
    }
}