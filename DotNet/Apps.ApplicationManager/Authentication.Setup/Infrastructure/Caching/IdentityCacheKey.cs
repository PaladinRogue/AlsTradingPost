using System;
using System.Collections.Generic;
using Common.ApplicationServices.Caching;
using Common.Resources.ValueObjects;

namespace Authentication.Setup.Infrastructure.Caching
{
    public class IdentityCacheKey<TKey, TValue> : ValueObject<IdentityCacheKey<TKey, TValue>> where TKey : CacheKey<TValue>
    {
        public IdentityCacheKey(
            TKey key,
            Guid identityId)
        {
            IdentityId = identityId;
            Key = key;
        }

        private TKey Key { get; }

        private Guid IdentityId { get; }

        public static implicit operator string(IdentityCacheKey<TKey, TValue> cacheKey)
        {
            return cacheKey.ToString();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ToString();
        }

        public override string ToString()
        {
            return $"{IdentityId}-{Key}";
        }
    }
}