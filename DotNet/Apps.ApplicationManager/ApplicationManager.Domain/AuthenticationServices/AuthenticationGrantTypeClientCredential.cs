using System;
using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeClientCredential : AuthenticationService
    {
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

        internal static AuthenticationGrantTypeClientCredential Create(
            CreateAuthenticationGrantTypeClientCredentialDdto createAuthenticationGrantTypeClientCredentialDdto)
        {
            return new AuthenticationGrantTypeClientCredential(createAuthenticationGrantTypeClientCredentialDdto);
        }
    }
}
