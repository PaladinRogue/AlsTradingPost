using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.Login.ClientCredential
{
    public interface IClientCredentialLoginCommand
    {
        Task<Identity> ExecuteAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ClientCredentialLoginCommandDdto clientCredentialLoginCommandDdto);
    }
}