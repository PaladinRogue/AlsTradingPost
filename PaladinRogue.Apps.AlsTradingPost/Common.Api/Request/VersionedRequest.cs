using Common.Api.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Api.Request
{
    public class VersionedRequest : IVersionedRequest
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
