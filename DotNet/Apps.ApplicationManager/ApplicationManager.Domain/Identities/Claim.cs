using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.Identities.ChangeClaim;
using ApplicationManager.Domain.Identities.CreateClaim;
using Common.Domain.Aggregates;
using Common.Resources;

namespace ApplicationManager.Domain.Identities
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

        public IAggregateRoot AggregateRoot => Identity;

        internal void Change(ChangeClaimDdto changeClaimDdto)
        {
            Value = changeClaimDdto.Value;
        }
    }
}
