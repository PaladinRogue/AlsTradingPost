using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Identities.Sessions
{
    public class Session : Entity, IAggregateMember
    {
        public Session()
        {
        }
        
        [MaxLength(100)]
        public string RefreshToken { get; protected set; }

        public bool IsRevoked { get; protected set; }

        public Guid IdentityId { get; protected set; }

        public Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}