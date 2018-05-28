using System;
using Common.Api.Resources;
using Common.Application.Authorisation;

namespace AlsTradingPost.Application.Trader.Authorisation
{
    public class TraderUpdateAuthorisationContext : IAuthorisationContext
    {
        public TraderUpdateAuthorisationContext(IEntityResource resource)
        {
            ResourceId = resource.Id;
        }

        public TraderUpdateAuthorisationContext(Guid traderId)
        {
            ResourceId = traderId;
        }

        public Type ResourceType => typeof(Domain.Models.Trader);
        public Guid? ResourceId { get; }
    }
}
