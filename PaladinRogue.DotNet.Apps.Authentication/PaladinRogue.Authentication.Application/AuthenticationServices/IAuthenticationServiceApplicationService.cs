using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models;

namespace PaladinRogue.Authentication.Application.AuthenticationServices
{
    public interface IAuthenticationServiceApplicationService
    {
        Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync(GetAuthenticationServicesAdto getAuthenticationServicesAdto);

        Task<IEnumerable<AuthenticationServiceTypeAdto>> GetAuthenticationServiceTypes();

        Task DeleteClientCredentialAsync(DeleteClientCredentialAdto deleteClientCredentialAdto);
    }
}