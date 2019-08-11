using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Application.Concurrency
{
    public interface IOutboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
