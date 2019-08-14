using PaladinRogue.Library.Core.Api.Concurrency.Interfaces;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Api.Concurrency
{
    public class VersionedResource : IVersionedResource
    {
        [Ignore]
        public IConcurrencyVersion Version { get; set; }
    }
}
