using System;
using Common.Application.Models;

namespace AlsTradingPost.Application.User.Models
{
    public class UserAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }
    }
}
