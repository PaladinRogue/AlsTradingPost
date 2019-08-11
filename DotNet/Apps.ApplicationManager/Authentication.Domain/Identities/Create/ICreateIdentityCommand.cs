using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Create
{
    public interface ICreateIdentityCommand
    {
        Task<Identity> ExecuteAsync();
    }
}
