using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using NodaTime;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.Identities.ValidateToken;
using PaladinRogue.Libray.Core.Common.Extensions;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Clocks;
using PaladinRogue.Libray.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Domain.Identities
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
            protected set => TokenHash = DataProtection.HashAsync(value).Result;
        }

        [Required]
        protected virtual HashSet TokenHash { get; set; }

        [Required]
        public Instant TokenExpiry { get; protected set; }

        [Required]
        public virtual Session Session { get; protected set; }

        public IAggregateRoot AggregateRoot => Session.Identity;

        public virtual AuthenticationGrantTypeRefreshToken AuthenticationGrantTypeRefreshToken { get; protected set; }

        internal async Task<bool> ValidateToken(ValidateRefreshTokenDdto validateRefreshTokenDdto)
        {
            return TokenHash == await DataProtection.HashAsync(validateRefreshTokenDdto.Token, TokenHash.Salt) && TokenExpiry >= Clock.Now();
        }
    }
}
