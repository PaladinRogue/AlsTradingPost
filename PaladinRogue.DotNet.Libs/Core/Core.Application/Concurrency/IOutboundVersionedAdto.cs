using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Application.Concurrency
{
    public interface IOutboundVersionedAdto : IVersionAdto<IConcurrencyVersion>
    {
    }
}
