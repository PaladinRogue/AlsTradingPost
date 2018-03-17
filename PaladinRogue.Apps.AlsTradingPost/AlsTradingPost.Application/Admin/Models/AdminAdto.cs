using System;
using Common.Application.Models;

namespace AlsTradingPost.Application.Admin.Models
{
    public class AdminAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
