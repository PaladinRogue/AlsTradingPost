using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Create
{
    public interface ICreateIdentityCommand
    {
        Task<Identity> ExecuteAsync();
    }
}
