using System;
using Common.Application.Concurrency;

namespace AlsTradingPost.Application.Admin.Models
{
    public class AdminAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }
    }
}
