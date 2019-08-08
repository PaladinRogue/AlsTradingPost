using System;
using System.Collections.Generic;
using Authorisation.Application.Contexts;
using Common.ApplicationServices.Caching;

namespace Authorisation.Application.Policies
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