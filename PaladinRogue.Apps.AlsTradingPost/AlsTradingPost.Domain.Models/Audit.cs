using System;

namespace AlsTradingPost.Domain.Models
{
    public class Audit
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid EntityId { get; set; }
        public string AuditedObject { get; set; }
    }
}