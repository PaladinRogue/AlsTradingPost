namespace ApplicationManager.Domain.Identities.CheckPassword
{
    public interface ICheckPasswordCommand
    {
        bool Execute(PasswordIdentity passwordIdentity, CheckPasswordDdto checkPasswordDdto);
    }
}