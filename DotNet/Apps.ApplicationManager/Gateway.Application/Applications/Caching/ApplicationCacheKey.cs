using System.Collections.Generic;
using Common.Application.Caching;
using Gateway.Domain.Applications;

namespace Gateway.Application.Applications.Caching
{
    public class ApplicationCacheKey : CacheKey<Domain.Applications.Application>
    {
        private const string ApplicationKey = nameof(Domain.Applications.Application);

        public ApplicationCacheKey(string name)
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
            return $"{ApplicationKey}-{Name}";
        }
    }
}