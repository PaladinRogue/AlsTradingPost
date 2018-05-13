namespace Common.Domain.DataProtection
{
    public class DataProtection
    {
        private static volatile IDataProtector _dataProtector;

        protected DataProtection()
        {
        }

        protected static IDataProtector DataProtector
        {
            get => _dataProtector;
            set => _dataProtector = value;
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
    }
}