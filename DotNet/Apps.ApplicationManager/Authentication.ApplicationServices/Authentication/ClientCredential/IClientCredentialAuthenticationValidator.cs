using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.ApplicationServices.Authentication.ClientCredential
{
    public interface IClientCredentialAuthenticationValidator
    {
        Task<IClientCredentialAuthenticationResult> Validate(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ValidateClientCredentialAdto validateClientCredentialAdto);
    }
}