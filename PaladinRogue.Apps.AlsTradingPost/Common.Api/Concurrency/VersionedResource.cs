using Common.Api.Concurrency.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;
using Newtonsoft.Json;

namespace Common.Api.Concurrency
{
    public class VersionedResource : IVersionedResource
    {
        [JsonIgnore]
        public IConcurrencyVersion Version { get; set; }
    }
}
