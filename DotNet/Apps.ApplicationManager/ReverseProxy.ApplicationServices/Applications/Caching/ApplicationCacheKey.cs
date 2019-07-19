using System.Collections.Generic;
using Common.ApplicationServices.Caching;
using ReverseProxy.Domain.Applications;

namespace ReverseProxy.ApplicationServices.Applications.Caching
{
    public class ApplicationCacheKey : CacheKey<Application>
    {
        private const string ApplicationKey = nameof(Application);

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