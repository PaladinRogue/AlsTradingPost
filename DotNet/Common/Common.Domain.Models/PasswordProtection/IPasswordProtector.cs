namespace Common.Domain.Models.PasswordProtection
{
    public interface IPasswordProtector
    {
        ProtectedPassword Protect<T>(T data, string salt = null);
    }
}