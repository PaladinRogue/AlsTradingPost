using Common.Api.Concurrency.Interfaces;
using Common.Api.Resources;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Api.Concurrency
{
    public class VersionedResource : IVersionedResource
    {
        [Ignore]
        public IConcurrencyVersion Version { get; set; }
    }
}
