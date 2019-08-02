﻿using System.ComponentModel.DataAnnotations;
using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace Authentication.Domain.Identities
{
    public abstract class AuthenticationIdentity : Entity, IAggregateMember
    {
        [Required]
        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}
