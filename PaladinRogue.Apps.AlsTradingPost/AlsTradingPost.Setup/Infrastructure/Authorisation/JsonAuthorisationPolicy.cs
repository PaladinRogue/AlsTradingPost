using System;
using AlsTradingPost.Resources.Authorization;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;
using Newtonsoft.Json.Linq;

namespace AlsTradingPost.Setup.Infrastructure.Authorisation
{
    public class JsonAuthorisationPolicy : IAuthorisationPolicy
    {
        private readonly IJsonAuthorisationPolicyProvider _jsonAuthorisationPolicyProvider;
        private readonly ICurrentUserProvider _currentUserProvider;

        public JsonAuthorisationPolicy(
            IJsonAuthorisationPolicyProvider jsonAuthorisationPolicyProvider,
            ICurrentUserProvider currentUserProvider)
        {
            _jsonAuthorisationPolicyProvider = jsonAuthorisationPolicyProvider;
            _currentUserProvider = currentUserProvider;
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
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(restriction));

            }
        }

        private bool CheckSelf(IAuthorisationContext authorisationContext)
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

            _currentUserProvider.WhoAmI.TryGetValue(authorisationContext.ResourceType, out Guid entityId);

            return entityId == authorisationContext.ResourceId;
        }
    }
}
