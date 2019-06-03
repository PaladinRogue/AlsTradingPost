using Common.Application.Concurrency.Interfaces;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Application.Concurrency
{
    public class OutboundVersionedAdto : IOutboundVersionedAdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
