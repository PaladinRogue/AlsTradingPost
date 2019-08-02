using System.ComponentModel.DataAnnotations;
using Authentication.Domain.AuthenticationServices.ChangeFacebook;
using Authentication.Domain.AuthenticationServices.CreateFacebook;
using Common.Domain.DataProtectors;
using Common.Resources;

namespace Authentication.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeFacebook : AuthenticationGrantTypeClientCredential
    {
        private const byte MaskLength = 8;
        private readonly string _clientMask = new string('*', MaskLength);

        protected AuthenticationGrantTypeFacebook()
        {
        }

        protected AuthenticationGrantTypeFacebook(CreateAuthenticationGrantTypeFacebookDdto createAuthenticationGrantTypeFacebookDdto)
        {
            Name = createAuthenticationGrantTypeFacebookDdto.Name;
            ClientId = createAuthenticationGrantTypeFacebookDdto.ClientId;
            ClientSecret = createAuthenticationGrantTypeFacebookDdto.ClientSecret;
            ClientGrantAccessTokenUrl = createAuthenticationGrantTypeFacebookDdto.ClientGrantAccessTokenUrl;
            GrantAccessTokenUrl = createAuthenticationGrantTypeFacebookDdto.GrantAccessTokenUrl;
            ValidateAccessTokenUrl = createAuthenticationGrantTypeFacebookDdto.ValidateAccessTokenUrl;
            AppAccessToken = createAuthenticationGrantTypeFacebookDdto.AppAccessToken;
        }

        [Required]
        [SensitiveInformation(DataKeys.AppAccessToken)]
        [MaxLength(FieldSizes.Default)]
        public string AppAccessToken { get; protected set; }

        public string MaskedAppAccessToken => _clientMask;

        internal static AuthenticationGrantTypeFacebook Create(
            CreateAuthenticationGrantTypeFacebookDdto createAuthenticationGrantTypeFacebookDdto)
        {
            return new AuthenticationGrantTypeFacebook(createAuthenticationGrantTypeFacebookDdto);
        }

        internal void Change(
            ChangeAuthenticationGrantTypeFacebookDdto changeAuthenticationGrantTypeFacebookDdto)
        {
            Name = changeAuthenticationGrantTypeFacebookDdto.Name;
            ClientId = changeAuthenticationGrantTypeFacebookDdto.ClientId;
            ClientSecret = changeAuthenticationGrantTypeFacebookDdto.ClientSecret;
            ClientGrantAccessTokenUrl = changeAuthenticationGrantTypeFacebookDdto.ClientGrantAccessTokenUrl;
            GrantAccessTokenUrl = changeAuthenticationGrantTypeFacebookDdto.GrantAccessTokenUrl;
            ValidateAccessTokenUrl = changeAuthenticationGrantTypeFacebookDdto.ValidateAccessTokenUrl;
            AppAccessToken = changeAuthenticationGrantTypeFacebookDdto.AppAccessToken;
        }
    }
}
