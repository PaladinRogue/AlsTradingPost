using PaladinRogue.Authentication.Domain.AuthenticationServices;

namespace PaladinRogue.Authentication.Application.Authentication.ClientCredential
{
    public interface IFacebookAuthenticationValidator : IClientCredentialAuthenticationValidator<AuthenticationGrantTypeFacebook>
    {
    }
}