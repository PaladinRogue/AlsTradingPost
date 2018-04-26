using Common.Api.Resources;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Api.Concurrency.Interfaces
{
    public interface IVersionedResource : IVersioned<IConcurrencyVersion>, IResource
    {
    }
}
