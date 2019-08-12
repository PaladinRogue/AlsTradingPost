using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Application.Concurrency
{
    public interface IInboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
