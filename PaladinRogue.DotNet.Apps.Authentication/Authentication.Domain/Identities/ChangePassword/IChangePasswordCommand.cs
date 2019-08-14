using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.ChangePassword
{
    public interface IChangePasswordCommand
    {
        Task ExecuteAsync(Identity identity,
            ChangePasswordCommandDdto changePasswordCommandDdto);
    }
}