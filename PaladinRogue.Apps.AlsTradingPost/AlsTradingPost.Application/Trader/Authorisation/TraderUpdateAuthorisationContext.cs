using System;
using Common.Application.Authorisation;

namespace AlsTradingPost.Application.Trader.Authorisation
{
    public class TraderUpdateAuthorisationContext : IAuthorisationContext
    {
        public TraderUpdateAuthorisationContext(Guid traderId)
        {
            ResourceId = traderId;
        }

        public Type ResourceType => typeof(Domain.Models.Trader);
        public Guid? ResourceId { get; }
    }
}
