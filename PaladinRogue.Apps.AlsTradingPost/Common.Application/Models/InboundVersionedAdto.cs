using Common.Application.Interfaces;

namespace Common.Application.Models
{
    public class InboundVersionedAdto : IInboundVersionedAdto
    {
        public int Version { get; set; }
    }
}
