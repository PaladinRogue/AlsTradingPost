using System;
using Common.Api.Concurrency.Interfaces;
using Common.Api.Meta;
using Common.Resources.Concurrency.Interfaces;
using Newtonsoft.Json;

namespace Common.Api.Concurrency
{
    public class VersionedResource : IVersionedResource
    {
        [ReadOnly]
        [Hidden]
        public Guid Id { get; set; }

        [JsonIgnore]
        public IConcurrencyVersion Version { get; set; }
    }
}
