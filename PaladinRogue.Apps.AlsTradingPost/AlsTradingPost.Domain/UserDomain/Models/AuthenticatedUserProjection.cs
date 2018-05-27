using System;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.UserDomain.Models
{
    public class AuthenticatedUserProjection : VersionedProjection
    {
        public Guid Id { get; set; }
    }
}
