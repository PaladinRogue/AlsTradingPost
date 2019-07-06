using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync();

        Task<ClientCredentialAdto> CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto);

        Task<ClientCredentialAdto> GetClientCredentialAsync(GetClientCredentialAdto getClientCredentialAdto);

        Task<ClientCredentialAdto> ChangeClientCredentialAsync(ChangeClientCredentialAdto changeClientCredentialAdto);
    }
}