using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IPasswordIdentityEmailExistsQuery
    {
        Task<bool> RunAsync(string emailAddress);
    }
}