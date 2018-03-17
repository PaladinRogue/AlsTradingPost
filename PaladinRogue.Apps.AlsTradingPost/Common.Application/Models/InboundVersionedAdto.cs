using Common.Application.Interfaces;

namespace Common.Application.Models
{
    public class InboundVersionedAdto : IInboundVersionedAdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
