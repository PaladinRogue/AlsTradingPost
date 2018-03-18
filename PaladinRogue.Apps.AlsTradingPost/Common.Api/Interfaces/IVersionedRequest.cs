
using Common.Resources.Concurrency;

namespace Common.Api.Interfaces
{
    public interface IVersionedRequest : IVersioned<IConcurrencyVersion>
    {
    }
}
