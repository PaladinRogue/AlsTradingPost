namespace ApplicationManager.Domain.Identities.Login.Password
{
    public interface IPasswordLoginCommand
    {
        Identity Execute(PasswordLoginCommandDdto passwordLoginCommandDdto);
    }
}