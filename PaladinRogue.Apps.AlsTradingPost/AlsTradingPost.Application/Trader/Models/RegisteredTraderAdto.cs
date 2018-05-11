using Common.Application.Concurrency;

namespace AlsTradingPost.Application.Trader.Models
{
    public class RegisteredTraderAdto : OutboundVersionedAdto
    {
        public string Alias { get; set; }
        public string DCI { get; set; }
    }
}
