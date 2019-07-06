using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IPasswordIdentityIdentifierExistsQuery
    {
        Task<bool> RunAsync(string identifier);
    }
}