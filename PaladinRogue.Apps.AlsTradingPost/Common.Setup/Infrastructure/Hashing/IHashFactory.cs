namespace Common.Setup.Infrastructure.Hashing
{
    public interface IHashFactory
    {
        Hashing GenerateHash<T>(T data);
    }
}