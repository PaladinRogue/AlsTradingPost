using System;
using System.Linq;
using Common.Domain.Models.Interfaces;
using Newtonsoft.Json.Linq;

namespace Common.Authorisation.Policies.Json
{
    public class JsonAuthorisationPolicy : IAuthorisationPolicy
    {
        private readonly IJsonAuthorisationPolicyProvider _jsonAuthorisationPolicyProvider;
        private readonly ISelfProvider _selfProvider;
        private readonly IResourceOwnerProvider _resourceOwnerProvider;

        public JsonAuthorisationPolicy(
            IJsonAuthorisationPolicyProvider jsonAuthorisationPolicyProvider,
            ISelfProvider selfProvider,
            IResourceOwnerProvider resourceOwnerProvider)
        {
            _jsonAuthorisationPolicyProvider = jsonAuthorisationPolicyProvider;
            _selfProvider = selfProvider;
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

            return  _selfProvider.WhoAmI.Any(i => i.Key == aggregateOwner.AggregateType && i.Value == aggregateOwner.Id);
        }

        private bool CheckSelf(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            _selfProvider.WhoAmI.TryGetValue(authorisationContext.ResourceType, out Guid entityId);

            return entityId == authorisationContext.ResourceId;
        }
    }
}
