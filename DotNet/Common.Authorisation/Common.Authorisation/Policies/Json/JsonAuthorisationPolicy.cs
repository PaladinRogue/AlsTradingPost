using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Authorisation.Contexts;
using Common.Authorisation.Restrictions;
using Common.Domain.Aggregates;
using Newtonsoft.Json.Linq;

namespace Common.Authorisation.Policies.Json
{
    public class JsonAuthorisationPolicy : IAuthorisationPolicy
    {
        private readonly IJsonAuthorisationPolicyProvider _jsonAuthorisationPolicyProvider;

        private readonly ISelfProvider _selfProvider;

        private readonly IResourceOwnerProviderCollection _resourceOwnerProviderCollection;

        private readonly IAuthorisationRestrictionProvider _authorisationRestrictionProvider;

        public JsonAuthorisationPolicy(
            IJsonAuthorisationPolicyProvider jsonAuthorisationPolicyProvider,
            ISelfProvider selfProvider,
            IResourceOwnerProviderCollection resourceOwnerProviderCollection,
            IAuthorisationRestrictionProvider authorisationRestrictionProvider)
        {
            _jsonAuthorisationPolicyProvider = jsonAuthorisationPolicyProvider;
            _selfProvider = selfProvider;
            _resourceOwnerProviderCollection = resourceOwnerProviderCollection;
            _authorisationRestrictionProvider = authorisationRestrictionProvider;
        }

        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            JToken policy = _jsonAuthorisationPolicyProvider.AuthorisationPolicy[authorisationContext.Resource][authorisationContext.Action];

            JToken restriction = policy[JTokenTypes.Restriction];

            string restrictionValue = restriction.Value<string>();

            switch (restrictionValue)
            {
                case ResourceRestriction.Everyone:
                    return Task.FromResult(true);
                case ResourceRestriction.Self:
                    return Task.FromResult(CheckSelf(authorisationContext));
                case ResourceRestriction.Owner:
                    return CheckOwnerAsync(authorisationContext);
                default:
                    return CheckPolicyAsync(restrictionValue, authorisationContext);
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

        private bool CheckSelf(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            _selfProvider.WhoAmI.TryGetValue(authorisationContext.ResourceType, out Guid entityId);

            return entityId == authorisationContext.ResourceId;
        }

        private async Task<bool> CheckOwnerAsync(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            IResourceOwnerProvider resourceOwnerProvider = _resourceOwnerProviderCollection.Get(authorisationContext.ResourceType);

            IAggregateOwner aggregateOwner = await resourceOwnerProvider.GetOwnerAsync(authorisationContext.ResourceId.Value);

            return  _selfProvider.WhoAmI.Any(i => i.Key == aggregateOwner.AggregateType && i.Value == aggregateOwner.Id);
        }

        private async Task<bool> CheckPolicyAsync(string restrictionValue, IAuthorisationContext authorisationContext)
        {
            IAuthorisationRestriction authorisationRestriction = _authorisationRestrictionProvider.GetByRestriction(restrictionValue);

            IRestrictionResult restrictionResult = await authorisationRestriction.CheckRestrictionAsync(authorisationContext);

            return restrictionResult.Succeeded;
        }
    }
}
