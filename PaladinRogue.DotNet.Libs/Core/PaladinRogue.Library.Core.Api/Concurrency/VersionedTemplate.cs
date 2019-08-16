using PaladinRogue.Library.Core.Api.Concurrency.Interfaces;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Api.Concurrency
{
    public class VersionedTemplate : IVersionedTemplate
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
