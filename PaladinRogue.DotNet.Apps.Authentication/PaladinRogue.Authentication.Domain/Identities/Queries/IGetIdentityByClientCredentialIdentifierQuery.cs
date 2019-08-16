using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.AuthenticationServices;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByClientCredentialIdentifierQuery
    {
        Task<Identity> RunAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential, string identifier);
    }
}