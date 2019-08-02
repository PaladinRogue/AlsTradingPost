using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Logout
{
    public interface ILogoutCommand
    {
        Task ExecuteAsync(Identity identity);
    }
}