using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Clocks;
using Common.Domain.DataProtection;
using Common.Resources.Extensions;
using NodaTime;

namespace ApplicationManager.Domain.Identities
{
    public class RefreshTokenIdentity : AuthenticationIdentity
    {
        private const byte MaskLength = 20;
        private readonly string _refreshTokenMask = new string('*', MaskLength);

        protected RefreshTokenIdentity()
        {
        }

        private RefreshTokenIdentity(
            Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken,
            out string token)
        {
            token = String.Random(100);
            Identity = identity;
            AuthenticationGrantTypeRefreshToken = authenticationGrantTypeRefreshToken;
            RefreshToken = token;
            TokenExpiry = Instant.Add(Clock.Now(),Duration.FromDays(2));
        }

        internal static RefreshTokenIdentity Create(
            Identity identity,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken,
            out string token)
        {
            return new RefreshTokenIdentity(identity, authenticationGrantTypeRefreshToken, out token);
        }

        public string RefreshToken
        {
            get => _refreshTokenMask;
            protected set => RefreshTokenHash = DataProtection.Hash(value);
        }

        [Required]
        protected virtual HashSet RefreshTokenHash { get; set; }

        [Required]
        public Instant TokenExpiry { get; protected set; }

        public virtual AuthenticationGrantTypeRefreshToken AuthenticationGrantTypeRefreshToken { get; protected set; }

        internal bool ValidateToken(ValidateRefreshTokenDdto validateRefreshTokenDdto)
        {
            return RefreshTokenHash == DataProtection.Hash(validateRefreshTokenDdto.Token, RefreshTokenHash.Salt) && TokenExpiry >= Clock.Now();
        }
    }
}
