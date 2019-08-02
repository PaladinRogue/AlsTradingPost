using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Queries
{
    public interface IPasswordIdentityIdentifierExistsQuery
    {
        Task<bool> RunAsync(string identifier);
    }
}