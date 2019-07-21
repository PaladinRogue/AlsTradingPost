using System;
using System.Collections.Generic;
using Common.ApplicationServices.Caching;
using KeyVault.Broker.Domain;

namespace KeyVault.Broker.ApplicationServices
{
    public class DataKeyCacheKey<T> : CacheKey<DataKey<T>> where T : Enum
    {
        private const string DataKey = nameof(DataKey<T>);

        public DataKeyCacheKey(T type)
        {
            Type = type;
        }

        private T Type { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
        }

        public override string ToString()
        {
            return $"{DataKey}-{nameof(T)}-{Type.ToString()}";
        }
    }
}