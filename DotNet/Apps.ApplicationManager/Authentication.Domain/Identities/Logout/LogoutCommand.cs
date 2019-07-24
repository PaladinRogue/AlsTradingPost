using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Logout
{
    public class LogoutCommand : ILogoutCommand
    {
        public Task ExecuteAsync(Identity identity)
        {
            identity.Logout();

            return Task.CompletedTask;
        }
    }
}