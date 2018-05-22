using System;
using Common.Domain.Models;
using Common.Domain.Models.DataProtection;

namespace AlsTradingPost.Domain.Models
{
    public class Audit : AggregateRoot
    {
        public DateTime Timestamp { get; set; }
        
        public Guid EntityId { get; set; }
        
        [SensitiveInformation]
        public string AuditedObject { get; set; }
    }
}