using Authentication.Domain.AuthenticationServices;

namespace Authentication.Application.Authentication.ClientCredential
{
    public interface IFacebookAuthenticationValidator : IClientCredentialAuthenticationValidator<AuthenticationGrantTypeFacebook>
    {
    }
}