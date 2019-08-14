using PaladinRogue.Library.Core.Common.ValueObjects;

namespace PaladinRogue.Library.Core.Application.Caching
{
    public abstract class CacheKey<TValue> : ValueObject<CacheKey<TValue>>
    {
        public static implicit operator string(CacheKey<TValue> cacheKey)
        {
            return cacheKey.ToString();
        }

        public abstract override string ToString();
    }
}