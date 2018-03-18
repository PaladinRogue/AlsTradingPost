using Common.Resources.Concurrency;

namespace Common.Api.Interfaces
{
    public interface IVersionedResource : IVersioned<IConcurrencyVersion>
    {
    }
}
