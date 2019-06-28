namespace ApplicationManager.Domain.Identities.Logout
{
    public class LogoutCommand : ILogoutCommand
    {
        public void Execute(Identity identity)
        {
            identity.Logout();
        }
    }
}