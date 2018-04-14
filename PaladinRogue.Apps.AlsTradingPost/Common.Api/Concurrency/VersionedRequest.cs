using Common.Api.Concurrency.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Api.Concurrency
{
    public class VersionedRequest : IVersionedRequest
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
