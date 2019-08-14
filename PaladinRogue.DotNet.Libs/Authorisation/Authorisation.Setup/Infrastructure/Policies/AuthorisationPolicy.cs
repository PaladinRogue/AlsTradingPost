using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Authorisation.Common.Policies;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Owners;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Restrictions;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Self;
using PaladinRogue.Library.Core.Domain.Aggregates;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies
{
    public class AuthorisationPolicy : IAuthorisationPolicy
    {
        private readonly IAuthorisationPolicyProvider _authorisationPolicyProvider;

        private readonly ISelfProvider _selfProvider;

        private readonly IResourceOwnerProviderCollection _resourceOwnerProviderCollection;

        private readonly IAuthorisationRestrictionProvider _authorisationRestrictionProvider;

        public AuthorisationPolicy(
            IAuthorisationPolicyProvider authorisationPolicyProvider,
            ISelfProvider selfProvider,
            IResourceOwnerProviderCollection resourceOwnerProviderCollection,
            IAuthorisationRestrictionProvider authorisationRestrictionProvider)
        {
            _authorisationPolicyProvider = authorisationPolicyProvider;
            _selfProvider = selfProvider;
            _resourceOwnerProviderCollection = resourceOwnerProviderCollection;
            _authorisationRestrictionProvider = authorisationRestrictionProvider;
        }

        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            ActionPolicy policy = _authorisationPolicyProvider.ResourcePolicies[authorisationContext.Resource][authorisationContext.Action];

            string restrictionValue = policy.Restriction;

            switch (restrictionValue)
            {
                case ResourceRestriction.Everyone:
                    return Task.FromResult(true);
                case ResourceRestriction.Self:
                    return CheckSelfAsync(authorisationContext);
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

            if (authorisationContext.ResourceType == null)
            {
                throw new ArgumentNullException(nameof(authorisationContext.ResourceType));
            }
        }

        private async Task<bool> CheckSelfAsync(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            IDictionary<Type,Guid> whoAmI = await _selfProvider.WhoAmIAsync();

            whoAmI.TryGetValue(authorisationContext.ResourceType, out Guid entityId);

            return entityId != Guid.Empty && entityId == authorisationContext.ResourceId;
        }

        private async Task<bool> CheckOwnerAsync(IAuthorisationContext authorisationContext)
        {
            ValidateAuthorisationContext(authorisationContext);

            IResourceOwnerProvider resourceOwnerProvider = _resourceOwnerProviderCollection.Get(authorisationContext.ResourceType);

            if (!authorisationContext.ResourceId.HasValue) return false;

            IAggregateOwner aggregateOwner = await resourceOwnerProvider.GetOwnerAsync(authorisationContext.ResourceId.Value);

            IDictionary<Type,Guid> whoAmI = await _selfProvider.WhoAmIAsync();

            return whoAmI.Any(i => i.Key == aggregateOwner.AggregateType && i.Value == aggregateOwner.Id);
        }

        private async Task<bool> CheckPolicyAsync(string restrictionValue, IAuthorisationContext authorisationContext)
        {
            IAuthorisationRestriction authorisationRestriction = _authorisationRestrictionProvider.GetByRestriction(restrictionValue);

            IRestrictionResult restrictionResult = await authorisationRestriction.CheckRestrictionAsync(authorisationContext);

            return restrictionResult.Succeeded;
        }
    }
}
