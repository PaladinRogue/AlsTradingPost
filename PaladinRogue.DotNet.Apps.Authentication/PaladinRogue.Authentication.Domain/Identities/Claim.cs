using System;
using System.ComponentModel.DataAnnotations;
using PaladinRogue.Authentication.Domain.Identities.ChangeClaim;
using PaladinRogue.Authentication.Domain.Identities.CreateClaim;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Domain.Aggregates;

namespace PaladinRogue.Authentication.Domain.Identities
{
    public class Claim : IAggregateMember
    {
        protected Claim()
        {
        }

        private Claim(
            Identity identity,
            CreateClaimDdto createClaimDdto)
        {
            Identity = identity;
            Type = createClaimDdto.Type;
            Value = createClaimDdto.Value;
        }

        internal static Claim Create(
            Identity identity,
            CreateClaimDdto createClaimDdto)
        {
            return new Claim(identity, createClaimDdto);
        }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Type { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Value { get; protected set; }

        [Required]
        public virtual Identity Identity { get; protected set; }

        public Guid IdentityId { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;

        internal void Change(ChangeClaimDdto changeClaimDdto)
        {
            Value = changeClaimDdto.Value;
        }
    }
}
