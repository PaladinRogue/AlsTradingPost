using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.AuthenticationServices;

namespace PaladinRogue.Authentication.Application.Authentication.ClientCredential
{
    public interface IClientCredentialAuthenticationValidator<in TClientCredential> where TClientCredential : AuthenticationGrantTypeClientCredential
    {
        Task<IClientCredentialAuthenticationResult> Validate(
            TClientCredential authenticationGrantTypeClientCredential,
            ValidateClientCredentialAdto validateClientCredentialAdto);
    }
}