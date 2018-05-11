using System;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.TraderDomain.Models
{
    public class TraderProjection : VersionedProjection
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string DCI { get; set; }
    }
}