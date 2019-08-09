using Authentication.Domain.AuthenticationServices;

namespace Authentication.Application.Authentication.ClientCredential
{
    public interface IGoogleAuthenticationValidator : IClientCredentialAuthenticationValidator<AuthenticationGrantTypeGoogle>
    {
    }
}