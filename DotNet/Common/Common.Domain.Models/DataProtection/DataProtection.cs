using Common.Setup.Infrastructure.Hashing;

namespace Common.Domain.Models.DataProtection
{
    public class DataProtection
    {
        private static volatile IDataProtector _dataProtector;

        private static volatile IDataHasher _dataHasher;

        protected DataProtection()
        {
        }

        protected static IDataProtector DataProtector
        {
            get => _dataProtector;
            set => _dataProtector = value;
        }

        protected static IDataHasher DataHasher
        {
            get => _dataHasher;
            set => _dataHasher = value;
        }

        public static void SetDataHasher(IDataHasher dataHasher)
        {
            if (DataHasher == null)
            {
                DataHasher = dataHasher;
            }
        }

        public static void SetDataProtector(IDataProtector dataProtector)
        {
            if (DataProtector == null)
            {
                DataProtector = dataProtector;
            }
        }

        public static string Protect<T>(T data)
        {
            if (DataProtector == null)
            {
                throw new DataProtectorNotSetException();
            }

            return DataProtector.Protect(data);
        }

        public static T Unprotect<T>(string data)
        {
            if (DataProtector == null)
            {
                throw new DataProtectorNotSetException();
            }

            return DataProtector.Unprotect<T>(data);
        }

        public static HashSet Hash(string data, string salt = null)
        {
            if (DataHasher == null)
            {
                throw new HashFactoryNotSetException();
            }

            return DataHasher.Hash(data, salt);
        }
    }
}