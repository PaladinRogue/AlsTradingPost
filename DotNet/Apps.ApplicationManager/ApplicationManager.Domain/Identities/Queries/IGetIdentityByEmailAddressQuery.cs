using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByEmailAddressQuery
    {
        Task<Identity> RunAsync(string emailAddress);
    }
}