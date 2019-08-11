using System;
using System.Collections.Generic;
using PaladinRogue.Libray.Authorisation.Common.Contexts;
using PaladinRogue.Libray.Core.Application.Caching;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies
{
    public class AuthorisationContextCacheKey : CacheKey<bool>, IAuthorisationContext
    {
        private const string AuthorisationContextKey = "AuthorisationContext";

        public AuthorisationContextCacheKey(IAuthorisationContext authorisationContext)
        {
            Resource = authorisationContext.Resource;
            Action = authorisationContext.Action;
            ResourceType = authorisationContext.ResourceType;
            ResourceId = authorisationContext.ResourceId;
        }

        public string Resource { get; }

        public string Action { get; }

        public Type ResourceType { get; }

        public Guid? ResourceId { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Resource;
            yield return Action;
            yield return ResourceType;
            yield return ResourceId;
        }

        public override string ToString()
        {
            return $"{AuthorisationContextKey}-{Resource}-{Action}-{ResourceType}-{ResourceId}";
        }
    }
}