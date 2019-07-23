using System.Threading.Tasks;
using Common.Domain.DataProtectors;
using KeyVault.Domain;
using KeyVault.Setup.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using DataKey = Common.Domain.DataProtectors.DataKey;

namespace KeyVault.Setup.Infrastructure.DataKeys
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