using PaladinRogue.Libray.Core.Api.Concurrency.Interfaces;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Api.Concurrency
{
    public class VersionedResource : IVersionedResource
    {
        [Ignore]
        public IConcurrencyVersion Version { get; set; }
    }
}
