using Common.Setup.Infrastructure.Hashing;

namespace Common.Resources.Hashing
{
    public interface IHashFactory
    {
        HashSet GenerateHash<T>(T data, string salt = null);
    }
}