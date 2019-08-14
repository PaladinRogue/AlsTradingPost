using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.ForgotPassword
{
    public interface IForgotPasswordCommand
    {
        Task<Identity> ExecuteAsync(ForgotPasswordCommandDdto forgotPasswordCommandDdto);
    }
}