using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByClientCredentialIdentifierQuery
    {
        Task<Identity> RunAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential, string identifier);
    }
}