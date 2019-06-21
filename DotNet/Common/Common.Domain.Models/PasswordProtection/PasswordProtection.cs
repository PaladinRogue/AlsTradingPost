namespace Common.Domain.Models.PasswordProtection
{
    public class PasswordProtection
    {
        private static volatile IPasswordProtector _passwordProtector;

        protected PasswordProtection()
        {
        }

        protected static IPasswordProtector PasswordProtector
        {
            get => _passwordProtector;
            set => _passwordProtector = value;
        }

        public static void SetPasswordProtector(IPasswordProtector passwordProtector)
        {
            if (PasswordProtector == null)
            {
                PasswordProtector = passwordProtector;
            }
        }

        public static ProtectedPassword Protect<T>(T password, string salt = null)
        {
            if (PasswordProtector == null)
            {
                throw new PasswordProtectorNotSetException();
            }

            return PasswordProtector.Protect(password, salt);
        }
    }
}