using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Logout
{
    public interface ILogoutCommand
    {
        Task ExecuteAsync(Identity identity);
    }
}