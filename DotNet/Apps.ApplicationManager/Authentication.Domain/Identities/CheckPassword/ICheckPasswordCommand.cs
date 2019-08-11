using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.CheckPassword
{
    public interface ICheckPasswordCommand
    {
        Task<bool> ExecuteAsync(PasswordIdentity passwordIdentity,
            CheckPasswordDdto checkPasswordDdto);
    }
}