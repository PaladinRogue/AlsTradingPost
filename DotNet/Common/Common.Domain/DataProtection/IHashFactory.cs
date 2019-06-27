namespace Common.Domain.DataProtection
{
    public interface IHashFactory
    {
        HashSet GenerateHash<T>(T data, string salt = null);
    }
}