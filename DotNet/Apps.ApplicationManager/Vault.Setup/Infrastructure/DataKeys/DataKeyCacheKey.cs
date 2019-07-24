using System.Collections.Generic;
using Common.ApplicationServices.Caching;
using Common.Domain.DataProtectors;

namespace Vault.Setup.Infrastructure.DataKeys
{
    public class DataKeyCacheKey : CacheKey<DataKey>
    {
        private const string DataKey = nameof(DataKey);

        public DataKeyCacheKey(string name)
        {
            Name = name;
        }

        private string Name { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public override string ToString()
        {
            return $"{DataKey}-{Name}";
        }
    }
}