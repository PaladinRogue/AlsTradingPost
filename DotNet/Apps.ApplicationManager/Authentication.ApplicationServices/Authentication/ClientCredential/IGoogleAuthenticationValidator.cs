using Authentication.Domain.AuthenticationServices;

namespace Authentication.ApplicationServices.Authentication.ClientCredential
{
    public interface IGoogleAuthenticationValidator : IClientCredentialAuthenticationValidator<AuthenticationGrantTypeGoogle>
    {
    }
}