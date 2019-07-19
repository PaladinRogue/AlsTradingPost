namespace Common.Domain.DataProtectors
{
    public interface IDataProtector
    {
        string Protect<T>(T data);

        T Unprotect<T>(string data);
    }
}