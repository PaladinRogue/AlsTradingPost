using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class User : VersionedEntity
    {
        public Guid IdentityId { get; set; }
    }
}
