using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ResetPassword
{
    public interface IResetPasswordCommand
    {
        Task<Identity> ExecuteAsync(ResetPasswordCommandDdto resetPasswordCommandDdto);
    }
}