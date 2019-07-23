using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using Common.Domain.DataProtectors;
using Common.Resources;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeClientCredential : AuthenticationService
    {
        private const byte MaskLength = 8;
        private readonly string _clientMask = new string('*', MaskLength);

        protected AuthenticationGrantTypeClientCredential()
        {
        }

        protected AuthenticationGrantTypeClientCredential(CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            Name = createAuthenticationGrantTypeClientCredentialDdto.Name;
            ClientId = createAuthenticationGrantTypeClientCredentialDdto.ClientId;
            ClientSecret = createAuthenticationGrantTypeClientCredentialDdto.ClientSecret;
            ClientGrantAccessTokenUrl = createAuthenticationGrantTypeClientCredentialDdto.ClientGrantAccessTokenUrl;
            GrantAccessTokenUrl = createAuthenticationGrantTypeClientCredentialDdto.GrantAccessTokenUrl;
            ValidateAccessTokenUrl = createAuthenticationGrantTypeClientCredentialDdto.ValidateAccessTokenUrl;
            AppAccessToken = createAuthenticationGrantTypeClientCredentialDdto.AppAccessToken;
        }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Name { get; protected set; }

        [SensitiveInformation(DataKeys.ClientId)]
        [MaxLength(FieldSizes.Default)]
        [Required]
        public string ClientId { get; protected set; }

        [Required]
        [SensitiveInformation(DataKeys.ClientSecret)]
        [MaxLength(FieldSizes.Default)]
        public string ClientSecret { get; protected set; }

        [Required]
        [SensitiveInformation(DataKeys.AppAccessToken)]
        [MaxLength(FieldSizes.Default)]
        public string AppAccessToken { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string ClientGrantAccessTokenUrl { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string GrantAccessTokenUrl { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string ValidateAccessTokenUrl { get; protected set; }

        public string MaskedClientId => _clientMask;

        public string MaskedClientSecret => _clientMask;

        public string MaskedAppAccessToken => _clientMask;

        internal static AuthenticationGrantTypeClientCredential Create(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            return new AuthenticationGrantTypeClientCredential(createAuthenticationGrantTypeClientCredentialDdto);
        }

        internal void Change(
            ChangeAuthenticationGrantTypeClientCredentialDdto changeAuthenticationGrantTypeClientCredentialDdto)
        {
            Name = changeAuthenticationGrantTypeClientCredentialDdto.Name;
            ClientId = changeAuthenticationGrantTypeClientCredentialDdto.ClientId;
            ClientSecret = changeAuthenticationGrantTypeClientCredentialDdto.ClientSecret;
            ClientGrantAccessTokenUrl = changeAuthenticationGrantTypeClientCredentialDdto.ClientGrantAccessTokenUrl;
            GrantAccessTokenUrl = changeAuthenticationGrantTypeClientCredentialDdto.GrantAccessTokenUrl;
            ValidateAccessTokenUrl = changeAuthenticationGrantTypeClientCredentialDdto.ValidateAccessTokenUrl;
            AppAccessToken = changeAuthenticationGrantTypeClientCredentialDdto.AppAccessToken;
        }
    }
}
