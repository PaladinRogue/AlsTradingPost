using Common.Application.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Application.Models
{
    public class InboundVersionedAdto : IInboundVersionedAdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
