using System;
using Common.Api.Resources;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

namespace AlsTradingPost.Application.Trader.Authorisation
{
    public class TraderUpdateAuthorisationContext : IAuthorisationContext
    {
        public TraderUpdateAuthorisationContext(Guid id)
        {
            ResourceId = id;
        }

        public TraderUpdateAuthorisationContext(IEntityResource resource)
        {
            ResourceId = resource.Id;
        }

        public Type ResourceType => typeof(Domain.Models.Trader);

        public Guid? ResourceId { get; }

        public string Resource => AuthorisationResource.Trader;

        public string Action => AuthorisationAction.Update;
    }
}
