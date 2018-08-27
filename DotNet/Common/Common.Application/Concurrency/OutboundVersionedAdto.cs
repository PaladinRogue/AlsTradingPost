using Common.Application.Concurrency.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Application.Concurrency
{
    public class OutboundVersionedAdto : IOutboundVersionedAdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
