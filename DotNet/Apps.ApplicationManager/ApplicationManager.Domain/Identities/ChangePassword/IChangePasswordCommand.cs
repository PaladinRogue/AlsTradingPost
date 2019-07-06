using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ChangePassword
{
    public interface IChangePasswordCommand
    {
        Task ExecuteAsync(Identity identity,
            ChangePasswordCommandDdto changePasswordCommandDdto);
    }
}