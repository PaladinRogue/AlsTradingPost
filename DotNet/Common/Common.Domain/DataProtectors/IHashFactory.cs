namespace Common.Domain.DataProtectors
{
    public interface IHashFactory
    {
        HashSet GenerateHash<T>(T data, string salt = null);
    }
}