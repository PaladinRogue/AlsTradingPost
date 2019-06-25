namespace ApplicationManager.Domain.Identities.ResetPassword
{
    public interface IResetPasswordCommand
    {
        Identity Execute(ResetPasswordCommandDdto resetPasswordCommandDdto);
    }
}