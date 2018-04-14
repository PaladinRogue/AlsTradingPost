
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Api.Concurrency.Interfaces
{
    public interface IVersionedRequest : IVersioned<IConcurrencyVersion>
    {
    }
}
