using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IPasswordIdentityIdentifierExistsQuery
    {
        Task<bool> RunAsync(string identifier);
    }
}