using System.Threading.Tasks;

namespace Authentication.Domain.Identities.ForgotPassword
{
    public interface IForgotPasswordCommand
    {
        Task<Identity> ExecuteAsync(ForgotPasswordCommandDdto forgotPasswordCommandDdto);
    }
}