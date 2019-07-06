using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ForgotPassword
{
    public interface IForgotPasswordCommand
    {
        Task<Identity> ExecuteAsync(ForgotPasswordCommandDdto forgotPasswordCommandDdto);
    }
}