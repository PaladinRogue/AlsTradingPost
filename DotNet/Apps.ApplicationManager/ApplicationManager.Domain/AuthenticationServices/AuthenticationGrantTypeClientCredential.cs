using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeClientCredential : AuthenticationService
    {
        public override string Discriminator
        {
            get => "CLIENT_CREDENTIAL";
            protected set => throw new NotSupportedException();
        }

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
        }
        
        [MaxLength(20)]
        public string Name { get; protected set; }

        [SensitiveInformation]
        public string ClientId { get; protected set; }

        [SensitiveInformation]
        public string ClientSecret { get; protected set; }

        public string ClientGrantAccessTokenUrl { get; protected set; }

        public string GrantAccessTokenUrl { get; protected set; }

        public string ValidateAccessTokenUrl { get; protected set; }

        public static AuthenticationGrantTypeClientCredential Create(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            return new AuthenticationGrantTypeClientCredential(createAuthenticationGrantTypeClientCredentialDdto);
        }
    }
}
