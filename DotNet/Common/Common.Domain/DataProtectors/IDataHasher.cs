namespace Common.Domain.DataProtectors
{
    public interface IDataHasher
    {
        HashSet Hash(string data, string salt = null);
    }
}