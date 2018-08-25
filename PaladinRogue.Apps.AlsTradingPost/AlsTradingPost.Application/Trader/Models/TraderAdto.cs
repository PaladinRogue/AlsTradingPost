using System;
using Common.Application.Concurrency;

namespace AlsTradingPost.Application.Trader.Models
{
    public class TraderAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }

        public string Alias { get; set; }

        public string Dci { get; set; }
    }
}