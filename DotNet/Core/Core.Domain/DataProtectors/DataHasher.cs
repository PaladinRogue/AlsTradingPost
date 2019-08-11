using System.Text;
using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public class DataHasher : IDataHasher
    {
        private readonly IHashFactory _hashFactory;

        private readonly IDataKeyProvider _dataKeyProvider;

        public DataHasher(IHashFactory hashFactory, IDataKeyProvider dataKeyProvider)
        {
            _hashFactory = hashFactory;
            _dataKeyProvider = dataKeyProvider;
        }

        public async Task<HashSet> StaticHashAsync(string data, string saltName)
        {
            DataKey dataKey = await _dataKeyProvider.GetAsync(saltName);
            return await _hashFactory.GenerateHashAsync(data, Encoding.UTF8.GetString(dataKey.Value.Key));
        }

        public Task<HashSet> HashAsync(string data, string salt = null)
        {
            return _hashFactory.GenerateHashAsync(data, salt);
        }
    }
}