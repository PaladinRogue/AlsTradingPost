using System;

namespace AlsTradingPost.Domain.TraderDomain.Models
{
    public class CreateTraderDdto
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string DCI { get; set; }
    }
}