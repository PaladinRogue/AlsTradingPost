using System;
using Common.Domain.Concurrency;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.AdminServices.Models
{
    public class AdminProjection : VersionedProjection
    {
        public Guid Id { get; set; }
    }
}
