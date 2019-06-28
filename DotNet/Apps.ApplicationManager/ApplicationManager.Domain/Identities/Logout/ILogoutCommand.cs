namespace ApplicationManager.Domain.Identities.Logout
{
    public interface ILogoutCommand
    {
        void Execute(Identity identity);
    }
}