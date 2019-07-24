using System.Threading.Tasks;

namespace Authentication.Domain.Identities.CheckPassword
{
    public interface ICheckPasswordCommand
    {
        Task<bool> ExecuteAsync(PasswordIdentity passwordIdentity,
            CheckPasswordDdto checkPasswordDdto);
    }
}