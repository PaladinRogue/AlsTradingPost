using Common.Resources.ValueObjects;

namespace Common.Application.Caching
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