using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByClientCredentialIdentifierQuery
    {
        Task<Identity> RunAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential, string identifier);
    }
}