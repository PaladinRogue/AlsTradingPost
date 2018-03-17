using Common.Api.Interfaces;

namespace Common.Api.Request
{
    public class VersionedRequest : IVersionedRequest
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
