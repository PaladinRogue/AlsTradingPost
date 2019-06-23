using Common.Domain.Models.DataProtection;
using Common.Resources.Hashing;
using Common.Setup.Infrastructure.Hashing;

namespace Common.Domain.DataProtection
{
    public class DataHasher : IDataHasher
    {
        private readonly IHashFactory _hashFactory;

        public DataHasher(IHashFactory hashFactory)
        {
            _hashFactory = hashFactory;
        }

        public HashSet Hash(string data, string salt = null)
        {
            return _hashFactory.GenerateHash(data, salt);
        }
    }
}