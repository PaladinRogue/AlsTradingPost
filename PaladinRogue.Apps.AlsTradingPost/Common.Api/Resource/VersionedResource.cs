using Common.Api.Interfaces;
using Newtonsoft.Json;

namespace Common.Api.Resource
{
    public class VersionedResource : IVersionedResource
    {
        [JsonIgnore]
        public IConcurrencyVersion Version { get; set; }
    }
}
