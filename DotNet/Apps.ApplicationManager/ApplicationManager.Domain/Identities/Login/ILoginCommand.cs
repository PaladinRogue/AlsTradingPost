namespace ApplicationManager.Domain.Identities.Login
{
    public interface ILoginCommand
    {
        Identity Execute(LoginCommandDdto loginCommandDdto);
    }
}