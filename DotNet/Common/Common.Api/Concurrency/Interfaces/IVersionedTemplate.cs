using Common.Api.Resources;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Api.Concurrency.Interfaces
{
    public interface IVersionedTemplate : IVersioned<IConcurrencyVersion>, ITemplate
    {
    }
}
