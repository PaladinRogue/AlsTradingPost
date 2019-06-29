using ApplicationManager.ApplicationServices.AuthenticationServices.Models;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        ClientCredentialAdto CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto);

        ClientCredentialAdto GetClientCredential(GetClientCredentialAdto getClientCredentialAdto);

        ClientCredentialAdto ChangeClientCredential(ChangeClientCredentialAdto changeClientCredentialAdto);
    }
}