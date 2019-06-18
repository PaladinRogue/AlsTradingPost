using Common.Domain.Models.PasswordProtection;
using Common.Resources.Hashing;
using Common.Setup.Infrastructure.Hashing;

namespace Common.Domain.PasswordProtection
{
    public class PasswordProtector : IPasswordProtector
    {
        private readonly IHashFactory _hashFactory;

        public PasswordProtector(
            IHashFactory hashFactory)
        {
            _hashFactory = hashFactory;
        }

        public ProtectedPassword Protect<T>(T data, string salt = null)
        {
            HashSet hashSet = _hashFactory.GenerateHash(data, salt);

            return ProtectedPassword.Create(hashSet.Hash, hashSet.Salt);
        }
    }
}