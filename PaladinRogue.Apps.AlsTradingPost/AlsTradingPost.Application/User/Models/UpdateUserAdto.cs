using System;
using Common.Application.Models;

namespace AlsTradingPost.Application.User.Models
{
    public class UpdateUserAdto : InboundVersionedAdto
    {
        public Guid Id { get; set; }
    }
}