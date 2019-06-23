using Common.Setup.Infrastructure.Hashing;

namespace Common.Domain.Models.DataProtection
{
    public interface IDataHasher
    {
        HashSet Hash(string data, string salt = null);
    }
}