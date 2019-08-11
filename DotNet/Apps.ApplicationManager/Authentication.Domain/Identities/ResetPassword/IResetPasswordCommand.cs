using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.ResetPassword
{
    public interface IResetPasswordCommand
    {
        Task<Identity> ExecuteAsync(ResetPasswordCommandDdto resetPasswordCommandDdto);
    }
}