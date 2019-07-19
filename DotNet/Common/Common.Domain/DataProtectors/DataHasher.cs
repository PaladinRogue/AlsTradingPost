namespace Common.Domain.DataProtectors
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