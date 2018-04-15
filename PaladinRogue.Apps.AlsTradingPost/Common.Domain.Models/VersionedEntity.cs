﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public abstract class VersionedEntity : IVersionedEntity
    {
        protected VersionedEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
