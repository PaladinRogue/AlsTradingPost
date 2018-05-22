using System;

namespace AlsTradingPost.Domain.TraderDomain.Models
{
    public class RegisterTraderDdto
    {
        public Guid UserId { get; set; }

        public string Alias { get; set; }
        
        public string DCI { get; set; }
    }
}