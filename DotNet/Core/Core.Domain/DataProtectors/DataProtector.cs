using System.Threading.Tasks;
using PaladinRogue.Libray.Core.Common.Encryption;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public class DataProtector : IDataProtector
    {
        private readonly IEncryptionFactory _encryptionFactory;

        private readonly IDataKeyProvider _dataKeyProvider;

        public DataProtector(
            IEncryptionFactory encryptionFactory,
            IDataKeyProvider dataKeyProvider)
        {
            _encryptionFactory = encryptionFactory;
            _dataKeyProvider = dataKeyProvider;
        }

        public async Task<string> ProtectAsync<T>(T data, string keyName)
        {
            DataKey dataKey = await _dataKeyProvider.GetAsync(keyName);

            return _encryptionFactory.Encrypt(data, dataKey.Value);
        }

        public async Task<T> UnprotectAsync<T>(string data, string keyName)
        {
            DataKey dataKey = await _dataKeyProvider.GetAsync(keyName);

            return _encryptionFactory.Decrypt<T>(data, dataKey.Value);
        }
    }
}