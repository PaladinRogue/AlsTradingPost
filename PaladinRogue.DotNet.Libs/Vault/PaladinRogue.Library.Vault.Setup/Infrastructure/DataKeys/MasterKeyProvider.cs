using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Vault.Domain;
using PaladinRogue.Library.Vault.Setup.Infrastructure.Settings;
using DataKey = PaladinRogue.Library.Core.Domain.DataProtectors.DataKey;

namespace PaladinRogue.Library.Vault.Setup.Infrastructure.DataKeys
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