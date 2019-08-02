using Common.Domain.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency
{
    public interface IOutboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
