using Common.Api.Concurrency.Interfaces;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Api.Concurrency
{
    public class VersionedTemplate : IVersionedTemplate
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
