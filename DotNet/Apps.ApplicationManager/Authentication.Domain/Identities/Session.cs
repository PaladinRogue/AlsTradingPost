using System.ComponentModel.DataAnnotations;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.Identities
{
    public class Session : Entity, IAggregateMember
    {
        protected Session()
        {
        }

        private Session(Identity identity)
        {
            Identity = identity;
            IsRevoked = false;
        }

        internal static Session Create(Identity identity)
        {
            return new Session(identity);
        }

        public bool IsRevoked { get; protected set; }

        public virtual RefreshToken RefreshToken { get; protected set; }

        [Required]
        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;

        internal RefreshToken CreateRefreshToken(AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken, out string token)
        {
            RefreshToken = RefreshToken.Create(this, authenticationGrantTypeRefreshToken, out token);

            return RefreshToken;
        }

        internal void Reinstate()
        {
            IsRevoked = false;
        }

        internal void Revoke()
        {
            RefreshToken = null;
            IsRevoked = true;
        }
    }
}