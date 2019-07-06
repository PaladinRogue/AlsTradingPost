using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Logout
{
    public interface ILogoutCommand
    {
        Task ExecuteAsync(Identity identity);
    }
}