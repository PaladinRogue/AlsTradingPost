﻿using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Identities
{
    public class Session : Entity, IAggregateMember
    {
        protected Session()
        {
        }

        public static Session Create()
        {
            return new Session();
        }
        
        [MaxLength(100)]
        public string RefreshToken { get; protected set; }

        public bool IsRevoked { get; protected set; }

        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}