using System.ComponentModel.DataAnnotations;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeClientCredential : AuthenticationService
    {
        protected AuthenticationGrantTypeClientCredential()
        {
        }

        public override string TypeDiscriminator => "ClientCredential";

        [MaxLength(20)]
        public string Name { get; protected set; }

        [SensitiveInformation]
        public string ClientId { get; protected set; }

        [SensitiveInformation]
        public string ClientSecret { get; protected set; }

        public string ClientGrantAccessTokenUrl { get; protected set; }

        public string GrantAccessTokenUrl { get; protected set; }

        public string ValidateAccessTokenUrl { get; protected set; }
    }
}
