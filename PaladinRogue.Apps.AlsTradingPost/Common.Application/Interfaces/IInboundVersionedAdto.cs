using Common.Resources.Concurrency;

namespace Common.Application.Interfaces
{
    public interface IInboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
