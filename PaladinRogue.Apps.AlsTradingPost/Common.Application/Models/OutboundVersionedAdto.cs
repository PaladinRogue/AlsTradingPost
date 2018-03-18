using Common.Application.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Application.Models
{
    public class OutboundVersionedAdto : IOutboundVersionedAdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
