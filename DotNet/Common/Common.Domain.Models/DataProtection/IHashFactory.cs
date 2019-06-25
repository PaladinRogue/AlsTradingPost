namespace Common.Domain.Models.DataProtection
{
    public interface IHashFactory
    {
        HashSet GenerateHash<T>(T data, string salt = null);
    }
}