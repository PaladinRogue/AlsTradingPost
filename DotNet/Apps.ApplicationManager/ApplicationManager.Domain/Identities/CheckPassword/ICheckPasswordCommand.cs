using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.CheckPassword
{
    public interface ICheckPasswordCommand
    {
        Task<bool> ExecuteAsync(PasswordIdentity passwordIdentity,
            CheckPasswordDdto checkPasswordDdto);
    }
}