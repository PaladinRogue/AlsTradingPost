using System.Threading.Tasks;

namespace Common.Domain.DataProtectors
{
    public static class DataProtection
    {
        private static volatile IDataProtector _dataProtector;

        private static volatile IDataHasher _dataHasher;

        private static IDataProtector DataProtector
        {
            get => _dataProtector;
            set => _dataProtector = value;
        }

        private static IDataHasher DataHasher
        {
            get => _dataHasher;
            set => _dataHasher = value;
        }

        public static void SetDataHasher(this IDataHasher dataHasher)
        {
            if (DataHasher == null)
            {
                DataHasher = dataHasher;
            }
        }

        public static void SetDataProtector(this IDataProtector dataProtector)
        {
            if (DataProtector == null)
            {
                DataProtector = dataProtector;
            }
        }

        public static Task<string> ProtectAsync<T>(T data, string keyName)
        {
            if (DataProtector == null)
            {
                throw new DataProtectorNotSetException();
            }

            return DataProtector.ProtectAsync(data, keyName);
        }

        public static Task<T> Unprotect<T>(string data, string keyName)
        {
            if (DataProtector == null)
            {
                throw new DataProtectorNotSetException();
            }

            return DataProtector.UnprotectAsync<T>(data, keyName);
        }

        public static HashSet Hash(string data, string salt = null)
        {
            if (DataHasher == null)
            {
                throw new DataHasherNotSetException();
            }

            return DataHasher.Hash(data, salt);
        }
    }
}