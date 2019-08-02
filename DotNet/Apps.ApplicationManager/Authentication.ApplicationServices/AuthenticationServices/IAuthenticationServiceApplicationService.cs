using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices.Models;

namespace Authentication.ApplicationServices.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync(GetAuthenticationServicesAdto getAuthenticationServicesAdto);

        Task<IEnumerable<AuthenticationServiceTypeAdto>> GetAuthenticationServiceTypes();

        Task DeleteClientCredentialAsync(DeleteClientCredentialAdto deleteClientCredentialAdto);
    }
}