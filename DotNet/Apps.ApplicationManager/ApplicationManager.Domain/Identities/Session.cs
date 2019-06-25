using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Identities
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

        [Required]
        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;

        internal void Reinstate()
        {
            IsRevoked = false;
        }
    }
}