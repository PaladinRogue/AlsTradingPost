using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices.Models;

namespace Authentication.ApplicationServices.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync();

        Task<ClientCredentialAdto> CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto);

        Task<ClientCredentialAdto> GetClientCredentialAsync(GetClientCredentialAdto getClientCredentialAdto);

        Task<ClientCredentialAdto> ChangeClientCredentialAsync(ChangeClientCredentialAdto changeClientCredentialAdto);
    }
}