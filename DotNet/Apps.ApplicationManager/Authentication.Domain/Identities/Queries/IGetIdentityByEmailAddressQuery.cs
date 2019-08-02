using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByEmailAddressQuery
    {
        Task<Identity> RunAsync(string emailAddress);
    }
}