using System.Threading.Tasks;
using Common.Domain.DataProtectors;
using Vault.Domain;
using Microsoft.Extensions.Options;
using Vault.Setup.Infrastructure.Settings;
using DataKey = Common.Domain.DataProtectors.DataKey;

namespace Vault.Setup.Infrastructure.DataKeys
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