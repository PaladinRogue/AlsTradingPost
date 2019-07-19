using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Aggregates;
using Common.Domain.Clocks;
using Common.Domain.DataProtectors;
using Common.Resources.Extensions;
using NodaTime;

namespace ApplicationManager.Domain.Identities
{
    public class RefreshToken : IAggregateMember
    {
        private const byte MaskLength = 20;
        private readonly string _tokenMask = new string('*', MaskLength);

        protected RefreshToken()
        {
        }

        private RefreshToken(
            Session session,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken,
            out string token)
        {
            token = String.Random(100);
            Session = session;
            AuthenticationGrantTypeRefreshToken = authenticationGrantTypeRefreshToken;
            Token = token;
            TokenExpiry = Instant.Add(Clock.Now(),Duration.FromDays(2));
        }

        internal static RefreshToken Create(
            Session session,
            AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken,
            out string token)
        {
            return new RefreshToken(session, authenticationGrantTypeRefreshToken, out token);
        }

        public string Token
        {
            get => _tokenMask;
            protected set => TokenHash = DataProtection.Hash(value);
        }

        [Required]
        protected virtual HashSet TokenHash { get; set; }

        [Required]
        public Instant TokenExpiry { get; protected set; }

        [Required]
        public virtual Session Session { get; protected set; }

        public IAggregateRoot AggregateRoot => Session.Identity;

        public virtual AuthenticationGrantTypeRefreshToken AuthenticationGrantTypeRefreshToken { get; protected set; }

        internal bool ValidateToken(ValidateRefreshTokenDdto validateRefreshTokenDdto)
        {
            return TokenHash == DataProtection.Hash(validateRefreshTokenDdto.Token, TokenHash.Salt) && TokenExpiry >= Clock.Now();
        }
    }
}
