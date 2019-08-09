using Common.Domain.Concurrency.Interfaces;

namespace Common.Application.Concurrency
{
    public interface IOutboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
