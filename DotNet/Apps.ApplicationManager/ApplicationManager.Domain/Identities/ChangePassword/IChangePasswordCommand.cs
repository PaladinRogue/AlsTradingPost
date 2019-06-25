namespace ApplicationManager.Domain.Identities.ChangePassword
{
    public interface IChangePasswordCommand
    {
        void Execute(Identity identity, ChangePasswordCommandDdto changePasswordCommandDdto);
    }
}