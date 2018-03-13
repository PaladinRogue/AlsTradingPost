using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models.Base
{
    public abstract class User : Entity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Identity Identity { get; set; }
    }
}
