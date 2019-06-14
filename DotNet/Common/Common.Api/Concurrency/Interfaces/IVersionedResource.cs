using Common.Api.Resources;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Api.Concurrency.Interfaces
{
    public interface IVersionedResource : IVersioned<IConcurrencyVersion>, IEntityResource
    {
    }
}
