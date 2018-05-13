using System;
using Common.Application.Concurrency;

namespace AlsTradingPost.Application.Trader.Models
{
    public class RegisteredTraderAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string DCI { get; set; }
    }
}
