using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Queries
{
    public interface IPasswordIdentityEmailExistsQuery
    {
        Task<bool> RunAsync(string emailAddress);
    }
}