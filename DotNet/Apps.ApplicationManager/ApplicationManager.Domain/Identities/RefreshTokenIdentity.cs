using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Domain.Models.DataProtection;
using Common.Resources.Extensions;

namespace ApplicationManager.Domain.Identities
{
    public class RefreshTokenIdentity : AuthenticationIdentity
    {
        protected RefreshTokenIdentity()
        {
        }

        protected RefreshTokenIdentity(
            Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken)
        {
            Identity = identity;
            AuthenticationGrantTypeRefreshToken = authenticationGrantTypeRefreshToken;
            RefreshToken = String.Random(100);
        }

        internal static RefreshTokenIdentity Create(
            Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken)
        {
            return new RefreshTokenIdentity(identity, authenticationGrantTypeRefreshToken);
        }

        [MaxLength(100)]
        [Required]
        [SensitiveInformation]
        public string RefreshToken { get; protected set; }

        public virtual AuthenticationGrantTypeRefreshToken AuthenticationGrantTypeRefreshToken { get; protected set; }
    }
}
