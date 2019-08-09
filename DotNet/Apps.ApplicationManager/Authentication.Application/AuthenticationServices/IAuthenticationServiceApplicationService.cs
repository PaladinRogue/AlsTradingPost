using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Application.AuthenticationServices.Models;

namespace Authentication.Application.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync(GetAuthenticationServicesAdto getAuthenticationServicesAdto);

        Task<IEnumerable<AuthenticationServiceTypeAdto>> GetAuthenticationServiceTypes();

        Task DeleteClientCredentialAsync(DeleteClientCredentialAdto deleteClientCredentialAdto);
    }
}