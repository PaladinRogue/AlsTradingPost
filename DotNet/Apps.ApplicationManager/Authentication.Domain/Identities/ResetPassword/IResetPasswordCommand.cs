using System.Threading.Tasks;

namespace Authentication.Domain.Identities.ResetPassword
{
    public interface IResetPasswordCommand
    {
        Task<Identity> ExecuteAsync(ResetPasswordCommandDdto resetPasswordCommandDdto);
    }
}