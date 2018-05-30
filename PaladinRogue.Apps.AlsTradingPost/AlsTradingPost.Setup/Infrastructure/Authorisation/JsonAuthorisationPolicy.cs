using System;
using System.Linq;
using AlsTradingPost.Resources.Authorization;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;
using Common.Domain.Models.Interfaces;
using Newtonsoft.Json.Linq;

namespace AlsTradingPost.Setup.Infrastructure.Authorisation
{
    public class JsonAuthorisationPolicy : IAuthorisationPolicy
    {
        private readonly IJsonAuthorisationPolicyProvider _jsonAuthorisationPolicyProvider;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IResourceOwnerProvider _resourceOwnerProvider;

        public JsonAuthorisationPolicy(
            IJsonAuthorisationPolicyProvider jsonAuthorisationPolicyProvider,
            ICurrentUserProvider currentUserProvider,
            IResourceOwnerProvider resourceOwnerProvider)
        {
            _jsonAuthorisationPolicyProvider = jsonAuthorisationPolicyProvider;
            _currentUserProvider = currentUserProvider;
            _resourceOwnerProvider = resourceOwnerProvider;
        }

        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            JToken policy = _jsonAuthorisationPolicyProvider.AuthorisationPolicy[authorisationContext.Resource][authorisationContext.Action];

            JToken restriction = policy[JTokenTypes.Restriction];

            switch (restriction.Value<string>())
            {
                case ResourceRestriction.Everyone:
                    return true;
                case ResourceRestriction.Self:
                    return CheckSelf(authorisationContext);
                case ResourceRestriction.Owner:
                    return CheckOwner(authorisationContext);
                default:
                    throw new ArgumentOutOfRangeException(nameof(restriction));

            }
        }

        private void ValidateAuthorisationContext(IAuthorisationContext authorisationContext)
        {
            if (authorisationContext == null)
            {
                throw new ArgumentNullException(nameof(authorisationContext));
            }

            if (authorisationContext.ResourceId == null)
            {
                throw new ArgumentNullException(nameof(authorisationContext.ResourceId));
            }

            if (authorisationContext.ResourceType == null)
            {
                throw new ArgumentNullException(nameof(authorisationContext.ResourceType));
            }
        }

        private bool CheckOwner(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            IAggregateOwner aggregateOwner = _resourceOwnerProvider.GetOwner(authorisationContext.ResourceType, authorisationContext.ResourceId.Value);

            return  _currentUserProvider.WhoAmI.Any(i => i.Key == aggregateOwner.AggregateType && i.Value == aggregateOwner.Id);
        }

        private bool CheckSelf(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            _currentUserProvider.WhoAmI.TryGetValue(authorisationContext.ResourceType, out Guid entityId);

            return entityId == authorisationContext.ResourceId;
        }
    }
}
