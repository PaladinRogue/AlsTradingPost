﻿using System;
using Common.Domain.Models.Interfaces;
using Newtonsoft.Json;

namespace AlsTradingPost.Domain.Models
{
    public class Audit
    {
        private Audit(IEntity entity)
        {
            Timestamp = DateTime.UtcNow;
            EntityId = entity.Id;
            AuditedObject = JsonConvert.SerializeObject(entity, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        }

        public DateTime Timestamp { get; }
        public Guid EntityId { get; }
        public string AuditedObject { get; }

        public static Audit Create(IEntity entity)
        {
            return new Audit(entity);
        }
    }
}