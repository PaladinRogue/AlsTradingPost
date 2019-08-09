using System;
using System.Collections.Generic;
using Common.Domain.Entities;

namespace Common.Application.Caching
{
    public class EntityCacheKey<TValue> : CacheKey<TValue> where TValue : IEntity
    {
        public EntityCacheKey(Guid id)
        {
            EntityId = id;
        }

        private Guid EntityId { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EntityId;
        }

        public override string ToString()
        {
            return $"{nameof(TValue)}-{EntityId}";
        }
    }
}