using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByIdentifierAndPasswordQuery
    {
        Task<Identity> RunAsync(string identifier, string password);
    }
}