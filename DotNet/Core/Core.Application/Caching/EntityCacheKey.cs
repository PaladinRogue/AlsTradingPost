using System;
using System.Collections.Generic;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Core.Application.Caching
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