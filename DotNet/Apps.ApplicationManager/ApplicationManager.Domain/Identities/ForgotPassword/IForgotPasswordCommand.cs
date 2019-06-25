namespace ApplicationManager.Domain.Identities.ForgotPassword
{
    public interface IForgotPasswordCommand
    {
        Identity Execute(ForgotPasswordCommandDdto forgotPasswordCommandDdto);
    }
}