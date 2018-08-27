using System;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.AdminDomain.Models
{
    public class AdminProjection : VersionedProjection
    {
        public Guid Id { get; set; }
    }
}
