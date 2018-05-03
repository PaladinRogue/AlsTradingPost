namespace Common.Setup.Infrastructure.Hashing
{
    public interface IHashFactory
    {
        HashSet GenerateHash<T>(T data);
    }
}