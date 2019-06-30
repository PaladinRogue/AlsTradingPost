using System.Collections.Generic;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        IEnumerable<AuthenticationServiceAdto> GetAuthenticationServices();

        ClientCredentialAdto CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto);

        ClientCredentialAdto GetClientCredential(GetClientCredentialAdto getClientCredentialAdto);

        ClientCredentialAdto ChangeClientCredential(ChangeClientCredentialAdto changeClientCredentialAdto);
    }
}