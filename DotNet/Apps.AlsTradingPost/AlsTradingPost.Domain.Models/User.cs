using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class User : AggregateRoot
    {
        public Guid IdentityId { get; set; }
    }
}
