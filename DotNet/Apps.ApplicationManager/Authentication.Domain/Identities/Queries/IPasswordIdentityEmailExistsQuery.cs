using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IPasswordIdentityEmailExistsQuery
    {
        Task<bool> RunAsync(string emailAddress);
    }
}