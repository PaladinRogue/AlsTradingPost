﻿using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Audit : Entity
    {
        public DateTime Timestamp { get; set; }
        public Guid EntityId { get; set; }
        public string AuditedObject { get; set; }
    }
}