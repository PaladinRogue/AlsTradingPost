namespace Common.Domain.DataProtection
{
    public interface IDataHasher
    {
        HashSet Hash(string data, string salt = null);
    }
}