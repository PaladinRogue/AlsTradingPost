namespace Common.Domain.DataProtection
{
    public interface IDataProtector
    {
        string Protect<T>(T data);

        T Unprotect<T>(string data);
    }
}