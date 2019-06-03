using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Application.Concurrency.Interfaces
{
    public interface IOutboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
