using Common.Resources.Encryption;
using Microsoft.Extensions.Options;

namespace Common.Domain.DataProtection
{
    public class DataProtector : IDataProtector
    {
        private readonly IEncryptionFactory _encryptionFactory;
        private readonly DataProtectionSettings _dataProtectionSettings;

        public DataProtector(
            IEncryptionFactory encryptionFactory,
            IOptions<DataProtectionSettings> dataProtectionSettingsAccessor)
        {
            _encryptionFactory = encryptionFactory;
            _dataProtectionSettings = dataProtectionSettingsAccessor.Value;
        }

        public string Protect<T>(T data)
        {
            return _encryptionFactory.Enrypt(data, _dataProtectionSettings.SigningKey);
        }

        public T Unprotect<T>(string data)
        {
            return _encryptionFactory.Decrypt<T>(data, _dataProtectionSettings.SigningKey);
        }
    }
}