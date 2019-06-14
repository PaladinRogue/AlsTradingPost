using Common.Domain.Concurrency.Interfaces;

namespace Common.Application.Concurrency
{
    public interface IInboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
