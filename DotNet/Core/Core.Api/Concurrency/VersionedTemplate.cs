using PaladinRogue.Libray.Core.Api.Concurrency.Interfaces;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Api.Concurrency
{
    public class VersionedTemplate : IVersionedTemplate
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
