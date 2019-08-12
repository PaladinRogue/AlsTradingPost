using System.Collections.Generic;
using PaladinRogue.Library.Core.Application.Caching;
using PaladinRogue.Library.Core.Domain.DataProtectors;

namespace PaladinRogue.Library.Vault.Application.Caching
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