using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Create
{
    public interface ICreateIdentityCommand
    {
        Task<Identity> ExecuteAsync();
    }
}
