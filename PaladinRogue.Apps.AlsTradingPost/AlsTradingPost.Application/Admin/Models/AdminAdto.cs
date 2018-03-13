using System;
using Common.Application.Interfaces;

namespace AlsTradingPost.Application.Admin.Models
{
    public class AdminAdto : IOutboundVersionedAdto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Version { get; set; }
    }
}
