using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Application.Concurrency.Interfaces
{
    public interface IOutboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
