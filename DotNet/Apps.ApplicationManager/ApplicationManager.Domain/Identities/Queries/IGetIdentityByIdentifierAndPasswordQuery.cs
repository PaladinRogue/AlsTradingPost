using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByIdentifierAndPasswordQuery
    {
        Task<Identity> RunAsync(string identifier, string password);
    }
}