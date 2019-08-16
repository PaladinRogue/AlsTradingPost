using PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeGoogle;
using PaladinRogue.Authentication.Domain.AuthenticationServices.CreateGoogle;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeGoogle : AuthenticationGrantTypeClientCredential
    {
        protected AuthenticationGrantTypeGoogle()
        {
        }

        protected AuthenticationGrantTypeGoogle(CreateAuthenticationGrantTypeGoogleDdto createAuthenticationGrantTypeGoogelDdto)
        {
            Name = createAuthenticationGrantTypeGoogelDdto.Name;
            ClientId = createAuthenticationGrantTypeGoogelDdto.ClientId;
            ClientSecret = createAuthenticationGrantTypeGoogelDdto.ClientSecret;
            ClientGrantAccessTokenUrl = createAuthenticationGrantTypeGoogelDdto.ClientGrantAccessTokenUrl;
            GrantAccessTokenUrl = createAuthenticationGrantTypeGoogelDdto.GrantAccessTokenUrl;
            ValidateAccessTokenUrl = createAuthenticationGrantTypeGoogelDdto.ValidateAccessTokenUrl;
        }

        internal static AuthenticationGrantTypeGoogle Create(
            CreateAuthenticationGrantTypeGoogleDdto createAuthenticationGrantTypeGoogleDdto)
        {
            return new AuthenticationGrantTypeGoogle(createAuthenticationGrantTypeGoogleDdto);
        }

        internal void Change(
            ChangeAuthenticationGrantTypeGoogleDdto changeAuthenticationGrantTypeGoogleDdto)
        {
            Name = changeAuthenticationGrantTypeGoogleDdto.Name;
            ClientId = changeAuthenticationGrantTypeGoogleDdto.ClientId;
            ClientSecret = changeAuthenticationGrantTypeGoogleDdto.ClientSecret;
            ClientGrantAccessTokenUrl = changeAuthenticationGrantTypeGoogleDdto.ClientGrantAccessTokenUrl;
            GrantAccessTokenUrl = changeAuthenticationGrantTypeGoogleDdto.GrantAccessTokenUrl;
            ValidateAccessTokenUrl = changeAuthenticationGrantTypeGoogleDdto.ValidateAccessTokenUrl;
        }
    }
}
