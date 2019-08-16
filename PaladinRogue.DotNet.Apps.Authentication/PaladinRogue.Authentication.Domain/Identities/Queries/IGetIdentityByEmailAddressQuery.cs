using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByEmailAddressQuery
    {
        Task<Identity> RunAsync(string emailAddress);
    }
}