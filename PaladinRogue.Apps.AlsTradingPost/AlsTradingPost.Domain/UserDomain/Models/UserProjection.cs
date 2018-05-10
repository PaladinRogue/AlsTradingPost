using System;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.UserDomain.Models
{
    public class UserProjection : VersionedProjection
    {
        public Guid Id { get; set; }
        public Guid IdentityId { get; set; }
    }
}
