using Common.Api.Concurrency.Interfaces;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Api.Concurrency
{
    public class VersionedTemplate : IVersionedTemplate
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
