using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.Application.Authentication.ClientCredential
{
    public interface IClientCredentialAuthenticationValidator<in TClientCredential> where TClientCredential : AuthenticationGrantTypeClientCredential
    {
        Task<IClientCredentialAuthenticationResult> Validate(
            TClientCredential authenticationGrantTypeClientCredential,
            ValidateClientCredentialAdto validateClientCredentialAdto);
    }
}