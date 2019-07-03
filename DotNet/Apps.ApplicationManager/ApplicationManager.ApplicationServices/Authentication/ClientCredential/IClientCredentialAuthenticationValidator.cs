using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.ApplicationServices.Authentication.ClientCredential
{
    public interface IClientCredentialAuthenticationValidator
    {
        Task<IClientCredentialAuthenticationResult> Validate(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ValidateClientCredentialAdto validateClientCredentialAdto);
    }
}